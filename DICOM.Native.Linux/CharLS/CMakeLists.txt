
#project(charls)

# The following compiler option are only meant for GCC:
if (CMAKE_COMPILER_IS_GNUCC)
    # the following is optimization micromanagement that made better code for x86
    # SET(CMAKE_CXX_FLAGS "-D NDEBUG -O3 -Wall -Wextra -pedantic -fvisibility=hidden -fomit-frame-pointer -momit-leaf-frame-pointer -fweb  -ftracer" )

    # Define GNU C++ defines for both Debug and Release
    set (CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++14 -Wall -Wextra -pedantic")

    # Define specific Debug settings.
    set (CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} -g")

    # Define specific Release settings.
    set (CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} -D NDEBUG -O3")
ENDIF ()

    # Define specific Debug settings.
    set (CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} -g")

    # Define specific Release settings.
    set (CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} -D NDEBUG -O3")



set (charls_PUBLIC_HEADERS charls.h publictypes.h)

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)

add_library(CharLS interface.cpp jpegls.cpp jpegmarkersegment.cpp jpegstreamreader.cpp jpegstreamwriter.cpp)
