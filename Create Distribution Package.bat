rmdir Distribute\ /S /Q
mkdir Distribute\
copy "DICOM\bin\Release\Dicom.dll" Distribute\ /Y
copy "DICOM\bin\Release\Dicom.pdb" Distribute\ /Y
copy "DICOM\bin\Release\Dicom.xml" Distribute\ /Y
copy "DICOM [Native]\Release\Dicom.Native.dll" Distribute\ /Y
copy "DICOM [Native]\Release\Dicom.Native64.dll" Distribute\ /Y
copy License.txt Distribute\ /Y
copy README.md Distribute\ /Y
copy ChangeLog.md Distribute\ /Y
copy "Examples\C-Store SCP\bin\Release\Dicom.CStoreSCP.exe" Distribute\ /Y
copy "Examples\DICOM Dump\bin\Release\Dicom.Dump.exe" Distribute\ /Y
copy "Examples\DICOM Media\bin\Release\Dicom.Media.exe" Distribute\ /Y
copy "packages\NLog.2.0.0.2000\lib\net40\NLog.dll" Distribute\ /Y