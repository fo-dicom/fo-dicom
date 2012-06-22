rmdir Distribute\ /S /Q
mkdir Distribute\
copy "DICOM\bin\Release\Dicom.dll" Distribute\ /Y
copy "DICOM\bin\Release\Dicom.xml" Distribute\ /Y
copy "DICOM [Native]\Release\Dicom.Native.dll" Distribute\ /Y
copy "DICOM [Native]\Release\Dicom.Native64.dll" Distribute\ /Y
copy License.txt Distribute\ /Y
copy README.md Distribute\ /Y
