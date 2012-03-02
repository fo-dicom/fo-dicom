// stdafx.h 

#ifndef STDAFX
#define STDAFX

#if defined(_WIN32)
#define CHARLS_IMEXPORT(returntype) __declspec(dllexport) returntype __stdcall
#endif

#include "util.h"

#endif
