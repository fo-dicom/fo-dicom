# Declare project
PROJECT(EFFERENT)

# Minimum CMake version required
CMAKE_MINIMUM_REQUIRED(VERSION 2.4)
IF(CMAKE_BACKWARDS_COMPATIBILITY GREATER 2.8.3)
  SET(CMAKE_BACKWARDS_COMPATIBILITY 2.8.3 CACHE STRING "Latest version of CMake when this project was released." FORCE)
ENDIF(CMAKE_BACKWARDS_COMPATIBILITY GREATER 2.8.3)

#-----------------------------------------------------------------------------
# General project settings to configure EFFERENT build process
#-----------------------------------------------------------------------------

# Modules to be built
SET(EFFERENT_MODULES  libijg8 libijg12 libijg16 CharLS OpenJPEG)

#-----------------------------------------------------------------------------
# Include directories
#-----------------------------------------------------------------------------

SET(EFFERENT_INCLUDE_DIR
 
  ${EFFERENT_SOURCE_DIR}/libijg8
  ${EFFERENT_SOURCE_DIR}/libijg12
  ${EFFERENT_SOURCE_DIR}/libijg16
  ${EFFERENT_SOURCE_DIR}/CharLS
  ${EFFERENT_SOURCE_DIR}/OpenJPEG

)
INCLUDE_DIRECTORIES(${EFFERENT_INCLUDE_DIR})

#-----------------------------------------------------------------------------
# Platform-independent settings
#-----------------------------------------------------------------------------

IF(COMMAND CMAKE_POLICY)
  # Enable new CMake behaviour when dealing with libraries whose full path is
  # given to the linker. This is necessary to avoid warnings in CMake versions
  # greater than 2.6.
  # See http://www.cmake.org/cmake/help/cmake-2.6.html#policy:CMP0003
  CMAKE_POLICY(SET CMP0003 NEW)

  # Works around warnings about escaped quotes in ADD_DEFINITIONS statements
  CMAKE_POLICY(SET CMP0005 OLD)
ENDIF(COMMAND CMAKE_POLICY)

# make OFString(NULL) safe by default
ADD_DEFINITIONS("-DUSE_NULL_SAFE_OFSTRING")

#-----------------------------------------------------------------------------
# Platform-specific settings
#-----------------------------------------------------------------------------

# Compiler flags for all other non-Windows systems
SET(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -D_XOPEN_SOURCE_EXTENDED -D_XOPEN_SOURCE=500 -D_BSD_SOURCE -D_BSD_COMPAT -D_OSF_SOURCE -D_POSIX_C_SOURCE=199506L")
SET(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -D_XOPEN_SOURCE_EXTENDED -D_XOPEN_SOURCE=500 -D_BSD_SOURCE -D_BSD_COMPAT -D_OSF_SOURCE -D_POSIX_C_SOURCE=199506L")

# add definition of "DEBUG" to debug mode (since CMake does not do this automatically)
SET(CMAKE_C_FLAGS_DEBUG "${CMAKE_C_FLAGS_DEBUG} -DDEBUG")
SET(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} -DDEBUG")



#-----------------------------------------------------------------------------
# Start actual compilation tasks
#-----------------------------------------------------------------------------

# Recurse into subdirectories
FOREACH(module ${EFFERENT_MODULES})
  ADD_SUBDIRECTORY(${module})
ENDFOREACH(module)


set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib})

