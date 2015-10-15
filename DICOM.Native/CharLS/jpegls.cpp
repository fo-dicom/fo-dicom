//
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
//

#include "util.h"
#include "decoderstrategy.h"
#include "encoderstrategy.h"
#include "lookuptable.h"
#include "losslesstraits.h"
#include "defaulttraits.h"
#include "jlscodecfactory.h"
#include "jpegstreamreader.h"


#include <cstdio>
#include <vector>
#include <iostream>

using namespace charls;


// As defined in the JPEG-LS standard 

// used to determine how large runs should be encoded at a time. 
const int J[32] = {0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 9, 10, 11, 12, 13, 14, 15};

#include "scan.h"


// Visual Studio 2013 does not supports the keyword noexcept. But it has the macro _NOEXCEPT.
#ifndef _NOEXCEPT
#define _NOEXCEPT noexcept
#endif


class charls_category : public std::error_category {
public:
    virtual const char* name() const _NOEXCEPT override
    {
        return "charls";
    }

    std::string message(int /* errval */) const override
    {
        return "CharLS error";
    }
};


const std::error_category& CharLSCategoryInstance()
{
    static charls_category instance;
    return instance;
}


signed char QuantizeGratientOrg(const JlsCustomParameters& preset, int32_t NEAR, int32_t Di)
{
    if (Di <= -preset.T3) return  -4;
    if (Di <= -preset.T2) return  -3;
    if (Di <= -preset.T1) return  -2;
    if (Di < -NEAR)  return  -1;
    if (Di <=  NEAR) return   0;
    if (Di < preset.T1)   return   1;
    if (Di < preset.T2)   return   2;
    if (Di < preset.T3)   return   3;

    return  4;
}


std::vector<signed char> CreateQLutLossless(int32_t cbit)
{
    JlsCustomParameters preset = ComputeDefault((1 << cbit) - 1, 0);
    int32_t range = preset.MAXVAL + 1;

    std::vector<signed char> lut(range * 2);
    
    for (int32_t diff = -range; diff < range; diff++)
    {
        lut[range + diff] = QuantizeGratientOrg(preset, 0,diff);
    }
    return lut;
}

// Lookup tables to replace code with lookup tables.
// To avoid threading issues, all tables are created when the program is loaded.


// Lookup table: decode symbols that are smaller or equal to 8 bit (16 tables for each value of k)
CTable decodingTables[16] = { InitTable(0), InitTable(1), InitTable(2), InitTable(3),
                              InitTable(4), InitTable(5), InitTable(6), InitTable(7),
                              InitTable(8), InitTable(9), InitTable(10), InitTable(11),
                              InitTable(12), InitTable(13), InitTable(14),InitTable(15) };


// Lookup tables: sample differences to bin indexes. 
std::vector<signed char> rgquant8Ll = CreateQLutLossless(8);
std::vector<signed char> rgquant10Ll = CreateQLutLossless(10);
std::vector<signed char> rgquant12Ll = CreateQLutLossless(12);
std::vector<signed char> rgquant16Ll = CreateQLutLossless(16);


template<typename STRATEGY>
std::unique_ptr<STRATEGY> JlsCodecFactory<STRATEGY>::GetCodec(const JlsParameters& info, const JlsCustomParameters& presets)
{
    std::unique_ptr<STRATEGY> strategy;

    if (presets.RESET != 0 && presets.RESET != BASIC_RESET)
    {
        DefaultTraitsT<uint8_t, uint8_t> traits((1 << info.bitspersample) - 1, info.allowedlossyerror);
        traits.MAXVAL = presets.MAXVAL;
        traits.RESET = presets.RESET;
        strategy = std::unique_ptr<STRATEGY>(new JlsCodec<DefaultTraitsT<uint8_t, uint8_t>, STRATEGY>(traits, info));
    }
    else
    {
        strategy = GetCodecImpl(info);
    }

    if (strategy)
    {
        strategy->SetPresets(presets);
    }
    return strategy;
}


template<typename TRAITS, typename STRATEGY>
std::unique_ptr<STRATEGY> CreateCodec(const TRAITS& t, const STRATEGY*, const JlsParameters& info)
{
    return std::unique_ptr<STRATEGY>(new JlsCodec<TRAITS, STRATEGY>(t, info));
}


template<typename STRATEGY>
std::unique_ptr<STRATEGY> JlsCodecFactory<STRATEGY>::GetCodecImpl(const JlsParameters& info)
{
    STRATEGY* s = nullptr;

    if (info.ilv == InterleaveMode::Sample && info.components != 3)
        return nullptr;

#ifndef DISABLE_SPECIALIZATIONS

    // optimized lossless versions common formats
    if (info.allowedlossyerror == 0)
    {
        if (info.ilv == InterleaveMode::Sample)
        {
            if (info.bitspersample == 8)
                return CreateCodec(LosslessTraitsT<Triplet<uint8_t>, 8>(), s, info);
        }
        else
        {
            switch (info.bitspersample)
            {
                case  8: return CreateCodec(LosslessTraitsT<uint8_t, 8>(), s, info);
                case 12: return CreateCodec(LosslessTraitsT<uint16_t, 12>(), s, info);
                case 16: return CreateCodec(LosslessTraitsT<uint16_t, 16>(), s, info);
            }
        }
    }

#endif

    int maxval = (1 << info.bitspersample) - 1;

    if (info.bitspersample <= 8)
    {
        if (info.ilv == InterleaveMode::Sample)
            return CreateCodec(DefaultTraitsT<uint8_t, Triplet<uint8_t> >(maxval, info.allowedlossyerror), s, info);

        return CreateCodec(DefaultTraitsT<uint8_t, uint8_t>((1 << info.bitspersample) - 1, info.allowedlossyerror), s, info);
    }
    else if (info.bitspersample <= 16)
    {
        if (info.ilv == InterleaveMode::Sample)
            return CreateCodec(DefaultTraitsT<uint16_t,Triplet<uint16_t> >(maxval, info.allowedlossyerror), s, info);

        return CreateCodec(DefaultTraitsT<uint16_t, uint16_t>(maxval, info.allowedlossyerror), s, info);
    }
    return nullptr;
}


template class JlsCodecFactory<DecoderStrategy>;
template class JlsCodecFactory<EncoderStrategy>;
