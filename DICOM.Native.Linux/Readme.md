# Dicom.Native.Linux

This is a proposal for implementing all the Dicom codec set for Linux (and possibly macOS). The specific platform used was Ubuntu 16.04 LTS / 64 bits.

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
Although all the mentioned components compile without errors, the resulting binaries are still not tested. Please advice if there are ready-made tests for the image-processing classes.

The make files and source code are tuned up for producing a 64-bit output. 32-bit hasn't been considered so far.

The source code for the Codecs, having filenames `Dicom.Imaging.Codec.*.cpp` and `.h`, are still untouched. The proposal is to convert them into plain-old C++ so they can be also compiled to any platform and p/invoked directly from `Dicom.Core` and probably use unsafe pointers for accessing the output buffers.
