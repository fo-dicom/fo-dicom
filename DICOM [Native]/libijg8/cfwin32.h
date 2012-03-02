/*
** Configuration definitions for the Microsoft Visual C++ Compiler 
** with x86 processor environment.
** Created manually.
** 
*/

#ifndef CFWIN32_H
#define CFWIN32_H

/* only process if running in a Win32 environment */
#ifdef _WIN32

/* Settings for Borland C++ Builder */
#ifdef __BORLANDC__
#define _MSC_VER 1200  // Treat Borland C++ 5.5 as MSVC6.
#endif /* __BORLANDC__ */

/* Compiling for windows */
#define windows 1
#define WIN32 1

/* Define if you have the <windows.h> header file.  */
#define HAVE_WINDOWS_H 1

/* Define if you have the <winsock.h> header file.  */
#define HAVE_WINSOCK_H 1

/* MinGW needs some special handling because it is a mixture of WIN32 and Posix. 
 * The following settings are not tested by configure but need to be set for MinGW.
 */
#ifdef __MINGW32__
#define HAVE_WINDOWS_H
#define HAVE_WINSOCK_H
#define NO_IOS_BASE_ASSIGN
#endif

/* Define the canonical host system type as a string constant */
#define CANONICAL_HOST_TYPE "i386-pc-win32"

/* Define if char is unsigned on the C compiler */
/* #undef  C_CHAR_UNSIGNED */

/* Define to the inline keyword supported by the C compiler, if any, or to the
   empty string */
#define C_INLINE __inline

/* Define if >> is unsigned on the C compiler */
/* #undef  C_RIGHTSHIFT_UNSIGNED */

/* Define the DCMTK default path */
#define DCMTK_PREFIX "C:\\usr\\local\\dicom"

/* Define the default data dictionary path for the dcmdata library package */
#define DCM_DICT_DEFAULT_PATH "dicom.dic;\\dicom.dic;" DCMTK_PREFIX "\\lib\\dicom.dic"

/* Define the environment variable path separator */
#define ENVIRONMENT_PATH_SEPARATOR ';'

/* Define to 1 if you have the `accept' function. */
#define HAVE_ACCEPT 1

/* Define to 1 if you have the `access' function. */
#define HAVE_ACCESS 1

/* Define to 1 if you have the <alloca.h> header file. */
/* #undef  HAVE_ALLOCA_H */

/* Define to 1 if you have the <arpa/inet.h> header file. */
/* #undef  HAVE_ARPA_INET_H */

/* Define to 1 if you have the <assert.h> header file. */
#define HAVE_ASSERT_H 1

/* Define to 1 if you have the `bcmp' function. */
/* #undef  HAVE_BCMP */

/* Define to 1 if you have the `bcopy' function. */
/* #undef  HAVE_BCOPY */

/* Define to 1 if you have the `bind' function. */
#define HAVE_BIND 1

/* Define to 1 if you have the `bzero' function. */
/* #undef  HAVE_BZERO */

/* Define if your C++ compiler can work with class templates */
#define HAVE_CLASS_TEMPLATE 1

/* Define to 1 if you have the `connect' function. */
#define HAVE_CONNECT 1

/* define if the compiler supports const_cast<> */
#define HAVE_CONST_CAST 1

/* Define to 1 if you have the <ctype.h> header file. */
#define HAVE_CTYPE_H 1

/* Define to 1 if you have the `cuserid' function. */
/* #undef  HAVE_CUSERID */

/* Define if bool is a built-in type */
#define HAVE_CXX_BOOL 1

/* Define if volatile is a known keyword */
#define HAVE_CXX_VOLATILE 1

/* Define if "const" is supported by the C compiler */
#define HAVE_C_CONST 1

/* Define if your system has a declaration for socklen_t in sys/types.h
   sys/socket.h */
/* #undef  HAVE_DECLARATION_SOCKLEN_T */

/* Define if your system has a declaration for std::ios_base::openmode in
   iostream.h */
/* #undef  HAVE_DECLARATION_STD__IOS_BASE__OPENMODE */

/* Define if your system has a declaration for struct utimbuf in sys/types.h
   utime.h sys/utime.h */
#define HAVE_DECLARATION_STRUCT_UTIMBUF 1

/* Define to 1 if you have the <dirent.h> header file, and it defines `DIR'.
   */
/* #undef  HAVE_DIRENT_H */

/* Define to 1 if you don't have `vprintf' but do have `_doprnt.' */
/* #undef  HAVE_DOPRNT */

/* define if the compiler supports dynamic_cast<> */
#define HAVE_DYNAMIC_CAST 1

/* Define if your system cannot pass command line arguments into main() (e.g. Macintosh) */
/* #undef HAVE_EMPTY_ARGC_ARGV */

/* Define to 1 if you have the <errno.h> header file. */
#define HAVE_ERRNO_H 1

/* Define if your C++ compiler supports the explicit template specialization
   syntax */
#define HAVE_EXPLICIT_TEMPLATE_SPECIALIZATION 1

/* Define to 1 if you have the <fcntl.h> header file. */
#define HAVE_FCNTL_H 1

/* Define to 1 if you have the `finite' function. */
/* #undef HAVE_FINITE */

/* Define to 1 if you have the <float.h> header file. */
#define HAVE_FLOAT_H 1

/* Define to 1 if you have the `flock' function. */
/* #undef  HAVE_FLOCK */

/* Define to 1 if you have the <fnmatch.h> header file. */
/* #undef  HAVE_FNMATCH_H */

/* Define to 1 if you have the `fork' function. */
/* #undef  HAVE_FORK */

/* Define to 1 if you have the <fstream> header file. */
#define HAVE_FSTREAM 1

/* Define to 1 if you have the <fstream.h> header file. */
#define HAVE_FSTREAM_H 1

/* Define if your C++ compiler can work with function templates */
#define HAVE_FUNCTION_TEMPLATE 1

/* Define to 1 if you have the `getenv' function. */
#define HAVE_GETENV 1

/* Define to 1 if you have the `geteuid' function. */
/* #undef  HAVE_GETEUID */

/* Define to 1 if you have the `getgrnam' function. */
/* #undef  HAVE_GETGRNAM */

/* Define to 1 if you have the `gethostbyname' function. */
#define HAVE_GETHOSTBYNAME 1

/* Define to 1 if you have the `gethostid' function. */
/* #undef  HAVE_GETHOSTID */

/* Define to 1 if you have the `gethostname' function. */
#define HAVE_GETHOSTNAME 1

/* Define to 1 if you have the `getlogin' function. */
/* #undef  HAVE_GETLOGIN */

/* Define to 1 if you have the `getpid' function. */
#define HAVE_GETPID 1

/* Define to 1 if you have the `getpwnam' function. */
/* #undef  HAVE_GETPWNAM */

/* Define to 1 if you have the `getsockname' function. */
#define HAVE_GETSOCKNAME 1

/* Define to 1 if you have the `getsockopt' function. */
#define HAVE_GETSOCKOPT 1

/* Define to 1 if you have the `getuid' function. */
/* #undef  HAVE_GETUID */

/* Define to 1 if you have the <grp.h> header file. */
/* #undef  HAVE_GRP_H */

/* Define to 1 if you have the <ieeefp.h> header file. */
/* #undef  HAVE_IEEEFP_H */

/* Define to 1 if you have the `index' function. */
/* #undef  HAVE_INDEX */

/* Define to 1 if the system has the type `int64_t'. */
/* #undef HAVE_INT64_T */

/* Define if your system declares argument 3 of accept() as int * instead of
   size_t * or socklen_t * */
#define HAVE_INTP_ACCEPT 1

/* Define if your system declares argument 5 of getsockopt() as int * instead
   of size_t * or socklen_t */
#define HAVE_INTP_GETSOCKOPT 1

/* Define if your system declares argument 2-4 of select() as int * instead of
   struct fd_set * */
/* #undef  HAVE_INTP_SELECT */

/* Define to 1 if you have the <inttypes.h> header file. */
/* #undef  HAVE_INTTYPES_H */

/* Define to 1 if you have the <iomanip> header file. */
#define HAVE_IOMANIP 1

/* Define to 1 if you have the <iomanip.h> header file. */
#define HAVE_IOMANIP_H 1

/* Define to 1 if you have the <iostream> header file. */
#define HAVE_IOSTREAM 1

/* Define to 1 if you have the <iostream.h> header file. */
#define HAVE_IOSTREAM_H 1

/* Define if your system defines ios::nocreate in iostream.h */
/* defined below */

/* Define to 1 if you have the <io.h> header file. */
#define HAVE_IO_H 1

/* Define to 1 if you have the `isinf' function. */
/* #undef  HAVE_ISINF */

/* Define to 1 if you have the `isnan' function. */
/* #undef HAVE_ISNAN */

/* Define to 1 if you have the <iso646.h> header file. */
#define HAVE_ISO646_H 1

/* Define to 1 if you have the `itoa' function. */
#define HAVE_ITOA 1

/* Define to 1 if you have the <libc.h> header file. */
/* #undef  HAVE_LIBC_H */

/* Define to 1 if you have the `iostream' library (-liostream). */
/* #undef  HAVE_LIBIOSTREAM */

/* Define to 1 if you have the `nsl' library (-lnsl). */
/* #undef  HAVE_LIBNSL */

/* Define to 1 if you have the <libpng/png.h> header file. */
/* #undef HAVE_LIBPNG_PNG_H */

/* Define to 1 if you have the `socket' library (-lsocket). */
/* #undef  HAVE_LIBSOCKET */

/* Define if libtiff supports LZW compression */
#define HAVE_LIBTIFF_LZW_COMPRESSION 1

/* Define to 1 if you have the <limits.h> header file. */
/* #undef  HAVE_LIMITS_H */

/* Define to 1 if you have the `listen' function. */
#define HAVE_LISTEN 1

/* Define to 1 if you have the <locale.h> header file. */
#define HAVE_LOCALE_H 1

/* Define to 1 if you have the `lockf' function. */
/* #undef  HAVE_LOCKF */

/* Define to 1 if the system has the type `longlong'. */
/* #undef HAVE_LONGLONG */

/* Define to 1 if you support file names longer than 14 characters. */
#define HAVE_LONG_FILE_NAMES 1

/* Define to 1 if you have the `malloc_debug' function. */
/* #undef  HAVE_MALLOC_DEBUG */

/* Define to 1 if you have the <malloc.h> header file. */
#define HAVE_MALLOC_H 1

/* Define to 1 if you have the <math.h> header file. */
#define HAVE_MATH_H 1

/* Define to 1 if you have the `memcmp' function. */
#define HAVE_MEMCMP 1

/* Define to 1 if you have the `memcpy' function. */
#define HAVE_MEMCPY 1

/* Define to 1 if you have the `memmove' function. */
#define HAVE_MEMMOVE 1

/* Define to 1 if you have the <memory.h> header file. */
/* #undef  HAVE_MEMORY_H */

/* Define to 1 if you have the `memset' function. */
#define HAVE_MEMSET 1

/* Define to 1 if you have the `mkstemp' function. */
/* #undef  HAVE_MKSTEMP */

/* Define to 1 if you have the `mktemp' function. */
#define HAVE_MKTEMP 1

/* Define to 1 if you have the <ndir.h> header file, and it defines `DIR'. */
/* #undef  HAVE_NDIR_H */

/* Define to 1 if you have the <netdb.h> header file. */
/* #undef  HAVE_NETDB_H */

/* Define to 1 if you have the <netinet/in.h> header file. */
/* #undef  HAVE_NETINET_IN_H */

/* Define to 1 if you have the <netinet/in_systm.h> header file. */
/* #undef  HAVE_NETINET_IN_SYSTM_H */

/* Define to 1 if you have the <netinet/tcp.h> header file. */
/* #undef  HAVE_NETINET_TCP_H */

/* Define to 1 if you have the <new.h> header file. */
#define HAVE_NEW_H 1

/* Define `pid_t' to `int' if <sys/types.h> does not define. */
#define HAVE_NO_TYPEDEF_PID_T 1
#ifdef HAVE_NO_TYPEDEF_PID_T
typedef int pid_t;
#endif

/* Define `size_t' to `unsigned' if <sys/types.h> does not define. */
/* #undef HAVE_NO_TYPEDEF_SIZE_T */
#ifdef HAVE_NO_TYPEDEF_SIZE_T
typedef unsigned size_t;
#endif

/* Define `ssize_t' to `long' if <sys/types.h> does not define. */
#define HAVE_NO_TYPEDEF_SSIZE_T 1
#ifdef HAVE_NO_TYPEDEF_SSIZE_T
typedef long ssize_t;
#endif

/* Define `uchar' to `unsigned char' if <sys/types.h> does not define. */
#define HAVE_NO_TYPEDEF_UCHAR 1
#ifdef HAVE_NO_TYPEDEF_UCHAR
typedef unsigned char uchar;
#endif

/* Define `uint' to `unsigned int' if <sys/types.h> does not define. */
#define HAVE_NO_TYPEDEF_UINT 1
#ifdef HAVE_NO_TYPEDEF_UINT
typedef unsigned int uint;
#endif

/* Define `ulong' to `unsigned long' if <sys/types.h> does not define. */
#define HAVE_NO_TYPEDEF_ULONG 1
#ifdef HAVE_NO_TYPEDEF_ULONG
typedef unsigned long ulong;
#endif

/* Define `ushort' to `unsigned short' if <sys/types.h> does not define. */
#define HAVE_NO_TYPEDEF_USHORT 1
#ifdef HAVE_NO_TYPEDEF_USHORT
typedef unsigned short ushort;
#endif

/* Define if your system supports readdir_r with the obsolete Posix 1.c draft
   6 declaration (2 arguments) instead of the Posix 1.c declaration with 3
   arguments. */
/* #undef  HAVE_OLD_READDIR_R */

/* Define if pthread_t is a pointer type on your system */
/* #undef HAVE_POINTER_TYPE_PTHREAD_T */

/* Define if your system has a prototype for accept in sys/types.h
   sys/socket.h */
#define HAVE_PROTOTYPE_ACCEPT 1

/* Define if your system has a prototype for bind in sys/types.h sys/socket.h
   */
#define HAVE_PROTOTYPE_BIND 1

/* Define if your system has a prototype for bzero in string.h strings.h
   libc.h unistd.h stdlib.h */
/* #undef  HAVE_PROTOTYPE_BZERO */

/* Define if your system has a prototype for connect in sys/types.h
   sys/socket.h */
#define HAVE_PROTOTYPE_CONNECT 1

/* Define if your system has a prototype for finite in math.h */
/* #undef HAVE_PROTOTYPE_FINITE */

/* Define if your system has a prototype for flock in sys/file.h */
/* #undef  HAVE_PROTOTYPE_FLOCK */

/* Define if your system has a prototype for gethostbyname in libc.h unistd.h
   stdlib.h netdb.h */
#define HAVE_PROTOTYPE_GETHOSTBYNAME 1

/* Define if your system has a prototype for gethostid in libc.h unistd.h
   stdlib.h netdb.h */
/* #undef  HAVE_PROTOTYPE_GETHOSTID */

/* Define if your system has a prototype for gethostname in unistd.h libc.h
   stdlib.h netdb.h */
#define HAVE_PROTOTYPE_GETHOSTNAME 1

/* Define if your system has a prototype for getsockname in sys/types.h
   sys/socket.h */
#define HAVE_PROTOTYPE_GETSOCKNAME 1

/* Define if your system has a prototype for getsockopt in sys/types.h
   sys/socket.h */
#define HAVE_PROTOTYPE_GETSOCKOPT 1

/* Define if your system has a prototype for gettimeofday in sys/time.h
   unistd.h */
/* #undef  HAVE_PROTOTYPE_GETTIMEOFDAY */

/* Define if your system has a prototype for isinf in math.h */
/* #undef HAVE_PROTOTYPE_ISINF */

/* Define if your system has a prototype for isnan in math.h */
/* #undef HAVE_PROTOTYPE_ISNAN */

/* Define if your system has a prototype for listen in sys/types.h
   sys/socket.h */
#define HAVE_PROTOTYPE_LISTEN 1

/* Define if your system has a prototype for mkstemp in libc.h unistd.h
   stdlib.h */
/* #undef HAVE_PROTOTYPE_MKSTEMP */

/* Define if your system has a prototype for mktemp in libc.h unistd.h
   stdlib.h */
/* #undef  HAVE_PROTOTYPE_MKTEMP */

/* Define if your system has a prototype for select in sys/select.h
   sys/types.h sys/socket.h sys/time.h */
#define HAVE_PROTOTYPE_SELECT 1

/* Define if your system has a prototype for setsockopt in sys/types.h
   sys/socket.h */
#define HAVE_PROTOTYPE_SETSOCKOPT 1

/* Define if your system has a prototype for socket in sys/types.h
   sys/socket.h */
#define HAVE_PROTOTYPE_SOCKET 1

/* Define if your system has a prototype for std::vfprintf in stdarg.h */
/* #undef HAVE_PROTOTYPE_STD__VFPRINTF  */

/* Define if your system has a prototype for strcasecmp in string.h */
/* #undef  HAVE_PROTOTYPE_STRCASECMP */

/* Define if your system has a prototype for strncasecmp in string.h */
/* #undef  HAVE_PROTOTYPE_STRNCASECMP */

/* Define if your system has a prototype for usleep in libc.h unistd.h
   stdlib.h */
/* #undef  HAVE_PROTOTYPE_USLEEP */

/* Define if your system has a prototype for wait3 in libc.h sys/wait.h
   sys/time.h sys/resource.h */
/* #undef  HAVE_PROTOTYPE_WAIT3 */

/* Define if your system has a prototype for waitpid in sys/wait.h sys/time.h
   sys/resource.h */
/* #undef  HAVE_PROTOTYPE_WAITPID */

/* Define if your system has a prototype for _stricmp in string.h */
#define HAVE_PROTOTYPE__STRICMP 1

/* Define to 1 if you have the <pthread.h> header file. */
/* #undef  HAVE_PTHREAD_H */

/* Define if your system supports POSIX read/write locks */
/* #undef  HAVE_PTHREAD_RWLOCK */

/* Define to 1 if you have the <pwd.h> header file. */
/* #undef  HAVE_PWD_H */

/* define if the compiler supports reinterpret_cast<> */
#define HAVE_REINTERPRET_CAST 1

/* Define to 1 if you have the `rindex' function. */
/* #undef  HAVE_RINDEX */

/* Define to 1 if you have the `select' function. */
#define HAVE_SELECT 1

/* Define to 1 if you have the <semaphore.h> header file. */
/* #undef  HAVE_SEMAPHORE_H */

/* Define to 1 if you have the <setjmp.h> header file. */
#define HAVE_SETJMP_H 1

/* Define to 1 if you have the `setsockopt' function. */
#define HAVE_SETSOCKOPT 1

/* Define to 1 if you have the `setuid' function. */
/* #undef  HAVE_SETUID */

/* Define to 1 if you have the <signal.h> header file. */
#define HAVE_SIGNAL_H 1

/* Define to 1 if you have the `sleep' function. */
/* #undef  HAVE_SLEEP */

/* Define to 1 if you have the `socket' function. */
#define HAVE_SOCKET 1

/* Define to 1 if you have the <sstream> header file. */
#define HAVE_SSTREAM 1

/* Define to 1 if you have the <sstream.h> header file. */
/* #undef  HAVE_SSTREAM_H */

/* Define to 1 if you have the `stat' function. */
#define HAVE_STAT 1

/* define if the compiler supports static_cast<> */
#define HAVE_STATIC_CAST 1

/* Define if your C++ compiler can work with static methods in class templates
   */
#define HAVE_STATIC_TEMPLATE_METHOD 1

/* Define to 1 if you have the <stat.h> header file. */
/* #undef  HAVE_STAT_H */

/* Define to 1 if you have the <stdarg.h> header file. */
#define HAVE_STDARG_H 1

/* Define to 1 if you have the <stdbool.h> header file. */
/* #undef HAVE_STDBOOL_H */

/* Define to 1 if you have the <stddef.h> header file. */
#define HAVE_STDDEF_H 1

/* Define to 1 if you have the <stdint.h> header file. */
/* #undef  HAVE_STDINT_H */

/* Define to 1 if you have the <stdio.h> header file. */
#define HAVE_STDIO_H 1

/* Define to 1 if you have the <stdlib.h> header file. */
#define HAVE_STDLIB_H 1

/* Define if ANSI standard C++ includes use std namespace */
/* defined below */

/* Define if the compiler supports std::nothrow */
/* defined below */

/* Define to 1 if you have the `strchr' function. */
#define HAVE_STRCHR 1

/* Define to 1 if you have the `strdup' function. */
/* #undef  HAVE_STRDUP */

/* Define to 1 if you have the `strerror' function. */
#define HAVE_STRERROR 1

/* Define to 1 if you have the <strings.h> header file. */
/* #undef  HAVE_STRINGS_H */

/* Define to 1 if you have the <string.h> header file. */
#define HAVE_STRING_H 1

/* Define to 1 if you have the `strlcat' function. */
/* #undef  HAVE_STRLCAT */

/* Define to 1 if you have the `strlcpy' function. */
/* #undef  HAVE_STRLCPY */

/* Define to 1 if you have the `strstr' function. */
#define HAVE_STRSTR 1

/* Define to 1 if you have the <strstream> header file. */
#define HAVE_STRSTREAM 1

/* Define to 1 if you have the <strstream.h> header file. */
/* #undef  HAVE_STRSTREAM_H */

/* Define to 1 if you have the <strstrea.h> header file. */
#define HAVE_STRSTREA_H 1

/* Define to 1 if you have the `strtoul' function. */
#define HAVE_STRTOUL 1

/* Define to 1 if you have the <synch.h> header file. */
/* #undef  HAVE_SYNCH_H */

/* Define to 1 if you have the `sysinfo' function. */
/* #undef  HAVE_SYSINFO */

/* Define to 1 if you have the <sys/dir.h> header file, and it defines `DIR'.
   */
/* #undef  HAVE_SYS_DIR_H */

/* Define to 1 if you have the <sys/errno.h> header file. */
/* #undef  HAVE_SYS_ERRNO_H */

/* Define to 1 if you have the <sys/file.h> header file. */
/* #undef  HAVE_SYS_FILE_H */

/* Define to 1 if you have the <sys/ndir.h> header file, and it defines `DIR'.
   */
/* #undef  HAVE_SYS_NDIR_H */

/* Define to 1 if you have the <sys/param.h> header file. */
/* #undef  HAVE_SYS_PARAM_H */

/* Define to 1 if you have the <sys/resource.h> header file. */
/* #undef  HAVE_SYS_RESOURCE_H */

/* Define to 1 if you have the <sys/select.h> header file. */
/* #undef  HAVE_SYS_SELECT_H */

/* Define to 1 if you have the <sys/socket.h> header file. */
/* #undef  HAVE_SYS_SOCKET_H */

/* Define to 1 if you have the <sys/stat.h> header file. */
#define HAVE_SYS_STAT_H 1

/* Define to 1 if you have the <sys/time.h> header file. */
/* #undef  HAVE_SYS_TIME_H */

/* Define to 1 if you have the <sys/types.h> header file. */
#define HAVE_SYS_TYPES_H 1

/* Define to 1 if you have the <sys/utime.h> header file. */
#define HAVE_SYS_UTIME_H 1

/* Define if variable-length arrays are supported in C */
/* #undef HAVE_VLA */

/* Define to 1 if you have the <sys/utsname.h> header file. */
/* #undef  HAVE_SYS_UTSNAME_H */

/* Define to 1 if you have <sys/wait.h> that is POSIX.1 compatible. */
/* #undef  HAVE_SYS_WAIT_H */

/* Define to 1 if you have the `tempnam' function. */
/* #undef  HAVE_TEMPNAM */

/* Define to 1 if you have the <thread.h> header file. */
/* #undef  HAVE_THREAD_H */

/* Define to 1 if you have the <time.h> header file. */
#define HAVE_TIME_H 1

/* Define to 1 if you have the `tmpnam' function. */
#define HAVE_TMPNAM 1

/* define if the compiler recognizes typename */
#define HAVE_TYPENAME 1

/* Define to 1 if the system has the type `uint64_t'. */
/* #undef HAVE_UINT64_T */

/* Define to 1 if the system has the type `ulonglong'. */
/* #undef HAVE_ULONGLONG */

/* Define to 1 if you have the `uname' function. */
/* #undef  HAVE_UNAME */

/* Define to 1 if you have the <unistd.h> header file. */
/* #undef  HAVE_UNISTD_H */

/* Define to 1 if you have the <unix.h> header file. */
/* #undef  HAVE_UNIX_H */

/* Define to 1 if you have the `usleep' function. */
/* #undef  HAVE_USLEEP */

/* Define to 1 if you have the <utime.h> header file. */
/* #undef  HAVE_UTIME_H */

/* Define to 1 if you have the `vprintf' function. */
#define HAVE_VPRINTF 1

/* Define to 1 if you have the `wait3' system call. Deprecated, you should no
   longer depend upon `wait3'. */
/* #undef  HAVE_WAIT3 */

/* Define to 1 if you have the `waitpid' function. */
/* #undef  HAVE_WAITPID */

/* Define to 1 if you have the <wctype.h> header file. */
#define HAVE_WCTYPE_H 1

/* Define to 1 if you have the `_findfirst' function. */
#define HAVE__FINDFIRST 1

/* Define if libc.h should be treated as a C++ header */
/* #undef INCLUDE_LIBC_H_AS_CXX */

/* Define if <math.h> fails if included extern "C" */
#define INCLUDE_MATH_H_AS_CXX 1

/* Define to the address where bug reports for this package should be sent. */
/* #undef  PACKAGE_BUGREPORT */

/* Define to the full name of this package. */
/* #undef  PACKAGE_NAME */

/* Define to the full name and version of this package. */
/* #undef  PACKAGE_STRING */

/* Define to the one symbol short name of this package. */
/* #undef  PACKAGE_TARNAME */

/* Define to the version of this package. */
/* #undef  PACKAGE_VERSION */

/* Define path separator */
#define PATH_SEPARATOR '\\'

/* Define as the return type of signal handlers (`int' or `void'). */
#define RETSIGTYPE void

/* Define if signal handlers need ellipse (...) parameters */
/* #undef  SIGNAL_HANDLER_WITH_ELLIPSE */

/* The size of a `char', as computed by sizeof. */
#define SIZEOF_CHAR 1

/* The size of a `double', as computed by sizeof. */
#define SIZEOF_DOUBLE 8

/* The size of a `float', as computed by sizeof. */
#define SIZEOF_FLOAT 4

/* The size of a `int', as computed by sizeof. */
#define SIZEOF_INT 4

/* The size of a `long', as computed by sizeof. */
#define SIZEOF_LONG 4

/* The size of a `short', as computed by sizeof. */
#define SIZEOF_SHORT 2

/* The size of a `void *', as computed by sizeof. */
#define SIZEOF_VOID_P 4

/* Define to 1 if you have the ANSI C header files. */
#define STDC_HEADERS 1

/* Define to 1 if your <sys/time.h> declares `struct tm'. */
/* #undef  TM_IN_SYS_TIME */

/* Define if ANSI standard C++ includes are used */
/* #undef  USE_STD_CXX_INCLUDES */

/* Define if we are compiling with libpng support */
/* #undef  WITH_LIBPNG */

/* Define if we are compiling with libtiff support */
/* #undef  WITH_LIBTIFF */

/* Define if we are compiling with libxml support */
/* #undef  WITH_LIBXML */

/* Define if we are compiling with OpenSSL support */
/* #undef  WITH_OPENSSL */

/* Define if we are compiling for built-in private tag dictionary */
/* #undef  WITH_PRIVATE_TAGS */

/* Define if we are compiling with libwrap (TCP wrapper) support */
/* #undef  WITH_TCPWRAPPER */

/* Define if we are compiling with any type of Multi-thread support */
#define WITH_THREADS

/* Define if we are compiling with zlib support */
/* #undef  WITH_ZLIB */

/* Define to 1 if on AIX 3.
   System headers sometimes define this.
   We just want to avoid a redefinition error message.  */
#ifndef _ALL_SOURCE
/* #undef _ALL_SOURCE */
#endif

/* Define to 1 if type `char' is unsigned and you are not using gcc.  */
#ifndef __CHAR_UNSIGNED__
/* #undef __CHAR_UNSIGNED__ */
#endif

/* Platform specific settings for Visual C++
 * By default, enable ANSI standard C++ includes on Visual C++ 6 and newer 
 *   _MSC_VER == 1100 on Microsoft Visual C++ 5.0 
 *   _MSC_VER == 1200 on Microsoft Visual C++ 6.0 
 *   _MSC_VER == 1300 on Microsoft Visual C++ 7.0 
 */
#ifdef _MSC_VER
#if _MSC_VER >= 1200
#define USE_STD_CXX_INCLUDES
#endif
#endif

#ifdef USE_STD_CXX_INCLUDES

/* Define if your system defines ios::nocreate in iostream.h */
/* #undef HAVE_IOS_NOCREATE  */

/* Define if ANSI standard C++ includes use std namespace */
#define HAVE_STD_NAMESPACE 1

/* Define if it is not possible to assign stream objects */
#define NO_IOS_BASE_ASSIGN 1

/* Define if the compiler supports std::nothrow */
#define HAVE_STD__NOTHROW 1

/* Define if your system has a prototype for std::vfprintf in stdarg.h */
/* #undef HAVE_PROTOTYPE_STD__VFPRINTF */

#else

/* Define if your system defines ios::nocreate in iostream.h */
#define HAVE_IOS_NOCREATE 1

/* Define if ANSI standard C++ includes use std namespace */
/* #undef HAVE_STD_NAMESPACE  */

/* Define if it is not possible to assign stream objects */
/* #undef NO_IOS_BASE_ASSIGN  */

/* Define if the compiler supports std::nothrow */
/* #undef HAVE_STD__NOTHROW  */

/* Define if your system has a prototype for std::vfprintf in stdarg.h */
/* #undef HAVE_PROTOTYPE_STD__VFPRINTF  */

#endif /* USE_STD_CXX_INCLUDES */


/* Additional settings for Borland C++ Builder */
#ifdef __BORLANDC__
#define _stricmp stricmp    // _stricmp in MSVC is stricmp in Borland C++
#define _strnicmp strnicmp  // _strnicmp in MSVC is strnicmp in Borland C++
#pragma warn -8027          // disable Warning W8027 "functions containing while are not expanded inline"
#pragma warn -8004          // disable Warning W8004 "variable is assigned a value that is never used"
#pragma warn -8012          // disable Warning W8012 "comparing signed and unsigned values"
#ifdef WITH_THREADS
#define __MT__              // required for _beginthreadex() API in <process.h>
#define _MT                 // required for _errno on BCB6
#endif
#define HAVE_PROTOTYPE_MKTEMP
#undef HAVE_SYS_UTIME_H 1
#define HAVE_UTIME_H

#endif /* __BORLANDC__ */

/* Additional settings for Visual Studio 2005 */
#ifdef _MSC_VER
#if _MSC_VER >= 1400
/* disable warnings about "deprecated" C runtime functions  */
#pragma warning( disable : 4996 )
#endif
#endif

#endif /* _WIN32 */

#endif /* CFWIN32_H */
