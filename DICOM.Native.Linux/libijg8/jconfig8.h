/*
 *
 *  Copyright (C) 1998-2010, OFFIS e.V.
 *  All rights reserved.  See COPYRIGHT file for details.
 *
 *  This software and supporting documentation were developed by
 *
 *    OFFIS e.V.
 *    R&D Division Health
 *    Escherweg 2
 *    D-26121 Oldenburg, Germany
 *
 *
 *  Module: dcmjpeg
 *
 *  Author: Marco Eichelberg
 *
 *  Purpose:
 *    this file derives the preprocessor symbols required to compile
 *    the IJG library from the central DCMTK configuration file osconfig.h
 *
 *  Last Update:      $Author: joergr $
 *  Update Date:      $Date: 2010-10-14 13:19:15 $
 *  CVS/RCS Revision: $Revision: 1.5 $
 *  Status:           $State: Exp $
 *
 *  CVS/RCS Log at end of file
 *
 */

#include "osconfig.h"

/* We assume ANSI C and don't support DOS, 
 * so the following settings need not be tested 
 */
#define HAVE_PROTOTYPES 
#define HAVE_UNSIGNED_CHAR 
#define HAVE_UNSIGNED_SHORT 
#undef NEED_FAR_POINTERS
#undef INCOMPLETE_TYPES_BROKEN

/* the following settings are derived from osconfig.h */

#ifndef HAVE_C_CONST
#define const
#endif

#ifdef C_CHAR_UNSIGNED
#define CHAR_IS_UNSIGNED
#endif

#ifdef HAVE_STRINGS_H
#ifndef HAVE_STRING_H
#define NEED_BSD_STRINGS
#endif
#endif

#ifdef HAVE_SYS_TYPES_H
#define NEED_SYS_TYPES_H
#endif

/* must always be defined for our implementation */
#define NEED_SHORT_EXTERNAL_NAMES

#ifdef JPEG_INTERNALS

#ifdef C_RIGHTSHIFT_UNSIGNED
#define RIGHT_SHIFT_IS_UNSIGNED
#endif

#define INLINE C_INLINE

/* These are for configuring the JPEG memory manager. */
#undef DEFAULT_MAX_MEM
#undef NO_MKTEMP

/* We don't want to use getenv which is thread unsafe on some platforms */
#define NO_GETENV

#endif /* JPEG_INTERNALS */

/*
 *  $Log: jconfig8.h,v $
 *  Revision 1.5  2010-10-14 13:19:15  joergr
 *  Updated copyright header. Added reference to COPYRIGHT file.
 *
 *  Revision 1.4  2005-12-08 15:47:55  meichel
 *  Updated Makefiles to correctly install header files
 *
 *  Revision 1.3  2001/12/18 09:48:54  meichel
 *  Modified configure test for "const" support of the C compiler
 *    in order to avoid a macro recursion error on Sun CC 2.0.1
 *
 *  Revision 1.2  2001/11/19 14:55:53  meichel
 *  Disabled JPEGMEM environment variable in dcmjpeg IJG code
 *    since it is not required and getenv() is thread unsafe on some systems.
 *
 *  Revision 1.1  2001/11/13 15:57:55  meichel
 *  Initial release of module dcmjpeg
 *
 *
 */
