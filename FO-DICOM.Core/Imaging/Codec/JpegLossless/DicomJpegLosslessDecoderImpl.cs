// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using System;
using System.IO;
using System.Text;

namespace FellowOakDicom.Imaging.Codec.JpegLossless
{

    internal class DicomJpegLosslessDecoderImpl : IDataStream, IDisposable
    {

        private ByteBufferByteSource _buffer;
        private FrameHeader _frame;
        private HuffmanTable _huffTable;
        private QuantizationTable _quantTable;
        private ScanHeader _scan;
        private readonly int[,][] _huffTab = JaggedArrayFactory.Create<int>(4, 2, MAX_HUFFMAN_SUBTREE * 256);
        private readonly int[] _idctSource = new int[64];
        private readonly int[] _nBlock = new int[10]; // number of blocks in the i-th Comp in a scan
        private readonly int[][] _acTab = new int[10][]; // ac HuffTab for the i-th Comp in a scan
        private readonly int[][] _dcTab = new int[10][]; // dc HuffTab for the i-th Comp in a scan
        private readonly int[][] _qTab = new int[10][]; // quantization table for the i-th Comp in a scan

        private bool _restarting;
        private int _marker;
        private int _markerIndex;
        private int _restartInterval;
        private int _selection;
        private int _xDim, _yDim;
        private int _xLoc;
        private int _yLoc;
        private int _mask;
        private int[] _outputData;
        private int[] _outputRedData;
        private int[] _outputGreenData;
        private int[] _outputBlueData;

        private static readonly int[] IDCT_P = { 0, 5, 40, 16, 45, 2, 7, 42, 21, 56, 8, 61, 18, 47, 1, 4, 41, 23, 58, 13, 32, 24, 37, 10, 63, 17, 44, 3, 6, 43, 20,
            57, 15, 34, 29, 48, 53, 26, 39, 9, 60, 19, 46, 22, 59, 12, 33, 31, 50, 55, 25, 36, 11, 62, 14, 35, 28, 49, 52, 27, 38, 30, 51, 54 };
        private static readonly int[] TABLE = { 0, 1, 5, 6, 14, 15, 27, 28, 2, 4, 7, 13, 16, 26, 29, 42, 3, 8, 12, 17, 25, 30, 41, 43, 9, 11, 18, 24, 31, 40, 44, 53,
            10, 19, 23, 32, 39, 45, 52, 54, 20, 22, 33, 38, 46, 51, 55, 60, 21, 34, 37, 47, 50, 56, 59, 61, 35, 36, 48, 49, 57, 58, 62, 63 };

        private static int RESTART_MARKER_BEGIN = 0xFFD0;
        private static int RESTART_MARKER_END = 0xFFD7;
        private static int MAX_HUFFMAN_SUBTREE = 50;

        private static int MSB = -2147483648; // 0x80000000


        public DicomJpegLosslessDecoderImpl(IByteBuffer data)
        {
            _buffer = new ByteBufferByteSource(data) { Endian = Endian.Big };
            _frame = new FrameHeader();
            _scan = new ScanHeader();
            _quantTable = new QuantizationTable();
            _huffTable = new HuffmanTable();
        }


        public int[][] Decode()
        {
            int current, scanNum = 0;
            int[] pred = new int[10];
            _xLoc = 0;
            _yLoc = 0;
            current = Get16();

            if (current != 0xFFD8)
            { // SOI
                throw new IOException("Not a JPEG file");
            }

            current = Get16();

            while (((current >> 4) != 0x0FFC) || (current == 0xFFC4))
            { // SOF 0~15
                switch (current)
                {
                    case 0xFFC4: // DHT
                        _huffTable.Read(this, _huffTab);
                        break;
                    case 0xFFCC: // DAC
                        throw new IOException("Program doesn't support arithmetic coding. (format throw new IOException)");
                    case 0xFFDB:
                        _quantTable.Read(this, TABLE);
                        break;
                    case 0xFFDD:
                        _restartInterval = ReadNumber();
                        break;
                    case 0xFFE0:
                    case 0xFFE1:
                    case 0xFFE2:
                    case 0xFFE3:
                    case 0xFFE4:
                    case 0xFFE5:
                    case 0xFFE6:
                    case 0xFFE7:
                    case 0xFFE8:
                    case 0xFFE9:
                    case 0xFFEA:
                    case 0xFFEB:
                    case 0xFFEC:
                    case 0xFFED:
                    case 0xFFEE:
                    case 0xFFEF:
                        ReadApp();
                        break;
                    case 0xFFFE:
                        ReadComment();
                        break;
                    default:
                        if ((current >> 8) != 0xFF)
                        {
                            throw new IOException("ERROR: format throw new IOException! (decode)");
                        }
                        break;
                }

                current = Get16();
            }

            if ((current < 0xFFC0) || (current > 0xFFC7))
            {
                throw new IOException("ERROR: could not handle arithmetic code!");
            }

            _frame.Read(this);
            current = Get16();
            int[][] outputRef;
            do
            {
                while (current != 0x0FFDA)
                { //SOS
                    switch (current)
                    {
                        case 0xFFC4: //DHT
                            _huffTable.Read(this, _huffTab);
                            break;
                        case 0xFFCC: //DAC
                            throw new IOException("Program doesn't support arithmetic coding. (format throw new IOException)");
                        case 0xFFDB:
                            _quantTable.Read(this, TABLE);
                            break;
                        case 0xFFDD:
                            _restartInterval = ReadNumber();
                            break;
                        case 0xFFE0:
                        case 0xFFE1:
                        case 0xFFE2:
                        case 0xFFE3:
                        case 0xFFE4:
                        case 0xFFE5:
                        case 0xFFE6:
                        case 0xFFE7:
                        case 0xFFE8:
                        case 0xFFE9:
                        case 0xFFEA:
                        case 0xFFEB:
                        case 0xFFEC:
                        case 0xFFED:
                        case 0xFFEE:
                        case 0xFFEF:
                            ReadApp();
                            break;
                        case 0xFFFE:
                            ReadComment();
                            break;
                        default:
                            if ((current >> 8) != 0xFF)
                            {
                                throw new IOException("ERROR: format throw new IOException! (Parser.decode)");
                            }
                            break;
                    }

                    current = Get16();
                }

                int precision = _frame.Precision;

                if (precision == 8)
                {
                    _mask = 0xFF;
                }
                else
                {
                    _mask = 0xFFFF;
                }

                ComponentSpec[] components = _frame.Components;

                _scan.Read(this);
                NumComponents = _scan.NumComponents;
                _selection = _scan.Selection;

                ScanComponent[] scanComps = _scan.components;
                int[][] quantTables = _quantTable.quantTables;

                for (int i = 0; i < NumComponents; i++)
                {
                    int compN = scanComps[i].ScanCompSel;
                    _qTab[i] = quantTables[components[compN].quantTableSel];
                    _nBlock[i] = components[compN].vSamp * components[compN].hSamp;
                    _dcTab[i] = _huffTab[scanComps[i].DcTabSel, 0];
                    _acTab[i] = _huffTab[scanComps[i].AcTabSel, 1];
                }

                _xDim = _frame.DimX;
                _yDim = _frame.DimY;

                outputRef = new int[NumComponents][];

                if (NumComponents == 1)
                {
                    _outputData = new int[_xDim * _yDim];
                    outputRef[0] = _outputData;
                }
                else
                {
                    _outputRedData = new int[_xDim * _yDim]; // not a good use of memory, but I had trouble packing bytes into int.  some values exceeded 255.
                    _outputGreenData = new int[_xDim * _yDim];
                    _outputBlueData = new int[_xDim * _yDim];

                    outputRef[0] = _outputRedData;
                    outputRef[1] = _outputGreenData;
                    outputRef[2] = _outputBlueData;
                }

                scanNum++;

                while (true)
                { // Decode one scan
                    int[] temp = new int[1]; // to store remainder bits
                    int[] index = new int[1];
                    temp[0] = 0;
                    index[0] = 0;

                    for (int i = 0; i < 10; i++)
                    {
                        pred[i] = (1 << (precision - 1));
                    }

                    if (_restartInterval == 0)
                    {
                        current = Decode(pred, temp, index);

                        while ((current == 0) && ((_xLoc < _xDim) && (_yLoc < _yDim)))
                        {
                            Output(pred);
                            current = Decode(pred, temp, index);
                        }

                        break; //current=MARKER
                    }

                    for (int mcuNum = 0; mcuNum < _restartInterval; mcuNum++)
                    {
                        _restarting = (mcuNum == 0);
                        current = Decode(pred, temp, index);
                        Output(pred);

                        if (current != 0)
                        {
                            break;
                        }
                    }

                    if (current == 0)
                    {
                        if (_markerIndex != 0)
                        {
                            current = (0xFF00 | _marker);
                            _markerIndex = 0;
                        }
                        else
                        {
                            current = Get16();
                        }
                    }

                    if ((current >= RESTART_MARKER_BEGIN) && (current <= RESTART_MARKER_END))
                    {
                        //empty
                    }
                    else
                    {
                        break; //current=MARKER
                    }
                }

                if ((current == 0xFFDC) && (scanNum == 1))
                { //DNL
                    ReadNumber();
                    current = Get16();
                }
            } while ((current != 0xFFD9) && ((_xLoc < _xDim) && (_yLoc < _yDim)) && (scanNum == 0));

            return outputRef;
        }


        public int Get16()
        {
            int value = (_buffer.GetInt16() & 0xFFFF);
            return value;
        }


        public int Get8()
        {
            return _buffer.GetUInt8() & 0xFF;
        }


        private int Decode(int[] prev, int[] temp, int[] index)
        {
            if (NumComponents == 1)
            {
                return DecodeSingle(prev, temp, index);
            }
            else if (NumComponents == 3)
            {
                return DecodeRGB(prev, temp, index);
            }
            else
            {
                return -1;
            }
        }


        private int DecodeSingle(int[] prev, int[] temp, int[] index)
        {
            //		At the beginning of the first line and
            //		at the beginning of each restart interval the prediction value of 2P – 1 is used, where P is the input precision.
            if (_restarting)
            {
                _restarting = false;
                prev[0] = (1 << (_frame.Precision - 1));
            }
            else
            {
                switch (_selection)
                {
                    case 2:
                        prev[0] = GetPreviousY(_outputData);
                        break;
                    case 3:
                        prev[0] = GetPreviousXY(_outputData);
                        break;
                    case 4:
                        prev[0] = (GetPreviousX(_outputData) + GetPreviousY(_outputData)) - GetPreviousXY(_outputData);
                        break;
                    case 5:
                        prev[0] = GetPreviousX(_outputData) + ((GetPreviousY(_outputData) - GetPreviousXY(_outputData)) >> 1);
                        break;
                    case 6:
                        prev[0] = GetPreviousY(_outputData) + ((GetPreviousX(_outputData) - GetPreviousXY(_outputData)) >> 1);
                        break;
                    case 7:
                        prev[0] = (int)(((long)GetPreviousX(_outputData) + GetPreviousY(_outputData)) / 2);
                        break;
                    default:
                        prev[0] = GetPreviousX(_outputData);
                        break;
                }
            }

            for (int i = 0; i < _nBlock[0]; i++)
            {
                int value = GetHuffmanValue(_dcTab[0], temp, index);

                if (value >= 0xFF00)
                {
                    return value;
                }

                int n = Getn(prev, value, temp, index);

                int nRestart = (n >> 8);
                if ((nRestart >= RESTART_MARKER_BEGIN) && (nRestart <= RESTART_MARKER_END))
                {
                    return nRestart;
                }

                prev[0] += n;
            }

            return 0;
        }


        private int DecodeRGB(int[] prev, int[] temp, int[] index)
        {
            switch (_selection)
            {
                case 2:
                    prev[0] = GetPreviousY(_outputRedData);
                    prev[1] = GetPreviousY(_outputGreenData);
                    prev[2] = GetPreviousY(_outputBlueData);
                    break;
                case 3:
                    prev[0] = GetPreviousXY(_outputRedData);
                    prev[1] = GetPreviousXY(_outputGreenData);
                    prev[2] = GetPreviousXY(_outputBlueData);
                    break;
                case 4:
                    prev[0] = (GetPreviousX(_outputRedData) + GetPreviousY(_outputRedData)) - GetPreviousXY(_outputRedData);
                    prev[1] = (GetPreviousX(_outputGreenData) + GetPreviousY(_outputGreenData)) - GetPreviousXY(_outputGreenData);
                    prev[2] = (GetPreviousX(_outputBlueData) + GetPreviousY(_outputBlueData)) - GetPreviousXY(_outputBlueData);
                    break;
                case 5:
                    prev[0] = GetPreviousX(_outputRedData) + ((GetPreviousY(_outputRedData) - GetPreviousXY(_outputRedData)) >> 1);
                    prev[1] = GetPreviousX(_outputGreenData) + ((GetPreviousY(_outputGreenData) - GetPreviousXY(_outputGreenData)) >> 1);
                    prev[2] = GetPreviousX(_outputBlueData) + ((GetPreviousY(_outputBlueData) - GetPreviousXY(_outputBlueData)) >> 1);
                    break;
                case 6:
                    prev[0] = GetPreviousY(_outputRedData) + ((GetPreviousX(_outputRedData) - GetPreviousXY(_outputRedData)) >> 1);
                    prev[1] = GetPreviousY(_outputGreenData) + ((GetPreviousX(_outputGreenData) - GetPreviousXY(_outputGreenData)) >> 1);
                    prev[2] = GetPreviousY(_outputBlueData) + ((GetPreviousX(_outputBlueData) - GetPreviousXY(_outputBlueData)) >> 1);
                    break;
                case 7:
                    prev[0] = (int)(((long)GetPreviousX(_outputRedData) + GetPreviousY(_outputRedData)) / 2);
                    prev[1] = (int)(((long)GetPreviousX(_outputGreenData) + GetPreviousY(_outputGreenData)) / 2);
                    prev[2] = (int)(((long)GetPreviousX(_outputBlueData) + GetPreviousY(_outputBlueData)) / 2);
                    break;
                default:
                    prev[0] = GetPreviousX(_outputRedData);
                    prev[1] = GetPreviousX(_outputGreenData);
                    prev[2] = GetPreviousX(_outputBlueData);
                    break;
            }

            int value;
            int[] actab;
            int[] dctab;
            int[] qtab;

            for (int ctrC = 0; ctrC < NumComponents; ctrC++)
            {
                qtab = _qTab[ctrC];
                actab = _acTab[ctrC];
                dctab = _dcTab[ctrC];
                for (int i = 0; i < _nBlock[ctrC]; i++)
                {
                    for (int k = 0; k < _idctSource.Length; k++)
                    {
                        _idctSource[k] = 0;
                    }

                    value = GetHuffmanValue(dctab, temp, index);

                    if (value >= 0xFF00)
                    {
                        return value;
                    }

                    prev[ctrC] = _idctSource[0] = prev[ctrC] + Getn(index, value, temp, index);
                    _idctSource[0] *= qtab[0];

                    for (int j = 1; j < 64; j++)
                    {
                        value = GetHuffmanValue(actab, temp, index);

                        if (value >= 0xFF00)
                        {
                            return value;
                        }

                        j += (value >> 4);

                        if ((value & 0x0F) == 0)
                        {
                            if ((value >> 4) == 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            _idctSource[IDCT_P[j]] = Getn(index, value & 0x0F, temp, index) * qtab[j];
                        }
                    }
                }
            }

            return 0;
        }


        //	Huffman table for fast search: (HuffTab) 8-bit Look up table 2-layer search architecture, 1st-layer represent 256 node (8 bits) if codeword-length > 8
        //	bits, then the entry of 1st-layer = (# of 2nd-layer table) | MSB and it is stored in the 2nd-layer Size of tables in each layer are 256.
        //	HuffTab[*][*][0-256] is always the only 1st-layer table.
        //	 
        //	An entry can be: (1) (# of 2nd-layer table) | MSB , for code length > 8 in 1st-layer (2) (Code length) << 8 | HuffVal
        //	 
        //	HuffmanValue(table   HuffTab[x][y] (ex) HuffmanValue(HuffTab[1][0],...)
        //	                ):
        //	    return: Huffman Value of table
        //	            0xFF?? if it receives a MARKER
        //	    Parameter:  table   HuffTab[x][y] (ex) HuffmanValue(HuffTab[1][0],...)
        //	                temp    temp storage for remainded bits
        //	                index   index to bit of temp
        //	                in      FILE pointer
        //	    Effect:
        //	        temp  store new remainded bits
        //	        index change to new index
        //	        in    change to new position
        //	    NOTE:
        //	      Initial by   temp=0; index=0;
        //	    NOTE: (explain temp and index)
        //	      temp: is always in the form at calling time or returning time
        //	       |  byte 4  |  byte 3  |  byte 2  |  byte 1  |
        //	       |     0    |     0    | 00000000 | 00000??? |  if not a MARKER
        //	                                               ^index=3 (from 0 to 15)
        //	                                               321
        //	    NOTE (marker and marker_index):
        //	      If get a MARKER from 'in', marker=the low-byte of the MARKER
        //	        and marker_index=9
        //	      If marker_index=9 then index is always > 8, or HuffmanValue()
        //	        will not be called
        private int GetHuffmanValue(int[] table, int[] temp, int[] index)
        {
            int code, input;
            int mask = 0xFFFF;

            if (index[0] < 8)
            {
                temp[0] <<= 8;
                input = Get8();
                if (input == 0xFF)
                {
                    _marker = Get8();
                    if (_marker != 0)
                    {
                        _markerIndex = 9;
                    }
                }
                temp[0] |= input;
            }
            else
            {
                index[0] -= 8;
            }

            code = table[temp[0] >> index[0]];

            if ((code & MSB) != 0)
            {
                if (_markerIndex != 0)
                {
                    _markerIndex = 0;
                    return 0xFF00 | _marker;
                }

                temp[0] &= (mask >> (16 - index[0]));
                temp[0] <<= 8;
                input = Get8();

                if (input == 0xFF)
                {
                    _marker = Get8();
                    if (_marker != 0)
                    {
                        _markerIndex = 9;
                    }
                }

                temp[0] |= input;
                code = table[((code & 0xFF) * 256) + (temp[0] >> index[0])];
                index[0] += 8;
            }

            index[0] += 8 - (code >> 8);

            if (index[0] < 0)
            {
                throw new IOException("index=" + index[0] + " temp=" + temp[0] + " code=" + code + " in HuffmanValue()");
            }

            if (index[0] < _markerIndex)
            {
                _markerIndex = 0;
                return 0xFF00 | _marker;
            }

            temp[0] &= (mask >> (16 - index[0]));
            return code & 0xFF;
        }


        private int Getn(int[] PRED, int n, int[] temp, int[] index)
        {
            int result;
            int one = 1;
            int n_one = -1;
            int mask = 0xFFFF;
            int input;

            if (n == 0)
            {
                return 0;
            }

            if (n == 16)
            {
                if (PRED[0] >= 0)
                {
                    return -32768;
                }
                else
                {
                    return 32768;
                }
            }

            index[0] -= n;

            if (index[0] >= 0)
            {
                if ((index[0] < _markerIndex) && !IsLastPixel())
                { // this was corrupting the last pixel in some cases
                    _markerIndex = 0;
                    return (0xFF00 | _marker) << 8;
                }

                result = temp[0] >> index[0];
                temp[0] &= (mask >> (16 - index[0]));
            }
            else
            {
                temp[0] <<= 8;
                input = Get8();

                if (input == 0xFF)
                {
                    _marker = Get8();
                    if (_marker != 0)
                    {
                        _markerIndex = 9;
                    }
                }

                temp[0] |= input;
                index[0] += 8;

                if (index[0] < 0)
                {
                    if (_markerIndex != 0)
                    {
                        _markerIndex = 0;
                        return (0xFF00 | _marker) << 8;
                    }

                    temp[0] <<= 8;
                    input = Get8();

                    if (input == 0xFF)
                    {
                        _marker = Get8();
                        if (_marker != 0)
                        {
                            _markerIndex = 9;
                        }
                    }

                    temp[0] |= input;
                    index[0] += 8;
                }

                if (index[0] < 0)
                {
                    throw new IOException("index=" + index[0] + " in getn()");
                }

                if (index[0] < _markerIndex)
                {
                    _markerIndex = 0;
                    return (0xFF00 | _marker) << 8;
                }

                result = temp[0] >> index[0];
                temp[0] &= (mask >> (16 - index[0]));
            }

            if (result < (one << (n - 1)))
            {
                result += (n_one << n) + 1;
            }

            return result;
        }


        private int GetPreviousX(int[] data)
        {
            if (_xLoc > 0)
            {
                return data[((_yLoc * _xDim) + _xLoc) - 1];
            }
            else if (_yLoc > 0)
            {
                return GetPreviousY(data);
            }
            else
            {
                return (1 << (_frame.Precision - 1));
            }
        }


        private int GetPreviousXY(int[] data)
        {
            if ((_xLoc > 0) && (_yLoc > 0))
            {
                return data[(((_yLoc - 1) * _xDim) + _xLoc) - 1];
            }
            else
            {
                return GetPreviousY(data);
            }
        }


        private int GetPreviousY(int[] data)
        {
            if (_yLoc > 0)
            {
                return data[((_yLoc - 1) * _xDim) + _xLoc];
            }
            else
            {
                return GetPreviousX(data);
            }
        }


        private bool IsLastPixel()
        {
            return (_xLoc == (_xDim - 1)) && (_yLoc == (_yDim - 1));
        }


        private void Output(int[] PRED)
        {
            if (NumComponents == 1)
            {
                OutputSingle(PRED);
            }
            else
            {
                OutputRGB(PRED);
            }
        }


        private void OutputSingle(int[] PRED)
        {
            if ((_xLoc < _xDim) && (_yLoc < _yDim))
            {
                _outputData[(_yLoc * _xDim) + _xLoc] = _mask & PRED[0];
                _xLoc++;

                if (_xLoc >= _xDim)
                {
                    _yLoc++;
                    _xLoc = 0;
                }
            }
        }


        private void OutputRGB(int[] PRED)
        {
            if ((_xLoc < _xDim) && (_yLoc < _yDim))
            {
                _outputRedData[(_yLoc * _xDim) + _xLoc] = PRED[0];
                _outputGreenData[(_yLoc * _xDim) + _xLoc] = PRED[1];
                _outputBlueData[(_yLoc * _xDim) + _xLoc] = PRED[2];
                _xLoc++;

                if (_xLoc >= _xDim)
                {
                    _yLoc++;
                    _xLoc = 0;
                }
            }
        }


        private int ReadApp()
        {
            int count = 0;
            int length = Get16();
            count += 2;

            while (count < length)
            {
                Get8();
                count++;
            }

            return length;
        }


        private string ReadComment()
        {
            var sb = new StringBuilder();
            int count = 0;

            int length = Get16();
            count += 2;

            while (count < length)
            {
                sb.Append((char)Get8());
                count++;
            }

            return sb.ToString();
        }


        private int ReadNumber()
        {
            int Ld = Get16();

            if (Ld != 4)
            {
                throw new IOException("ERROR: Define number format throw new IOException [Ld!=4]");
            }

            return Get16();
        }


        public int NumComponents { get; private set; }

        public int Precision => _frame.Precision;

        public int DimX => _frame.DimX;
        public int DimY => _frame.DimY;

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _huffTable = null;
                    _quantTable = null;
                    _quantTable = null;
                    _scan = null;
                    _frame = null;
                    _buffer = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~JPEGLosslessDecoder()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }

}
