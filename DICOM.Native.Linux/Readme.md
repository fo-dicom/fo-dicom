# Dicom.Native.Linux

This is a proposal for implementing all the Dicom codec set for Linux (and possibly macOS).

## What is already done
The following projects are now compilable for the Ubuntu platform:
- CharLS
- OpenJPEG
- Libijg8
- Libijg12
- Libijg16

All the C++ and headers are adjusted for the g++ compiler and thus it may be possible to compile for macOS as well (still untested)

## Compilation instructions
- In Ubuntu, open a Terminal window
- Change the working directory to DICOM.Native.Linux's root folder
- Type the following commands:
````text
    cmake .
    make
````

## What is missing
Although all the mentioned components compile, the resulting binaries are still not tested. Please advice if there are ready-made tests for the image-processing classes.

The source code for the Codecs, with filenames `Dicom.Imaging.Codec.*.cpp` and `.h`, are still untouched. The proposal is to convert them into plain-old C++ so they can be also compiled to any platform and p/invoked directly from `Dicom.Core`.
