// 
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
// 


#ifndef CHARLS_DEFAULTTRAITS
#define CHARLS_DEFAULTTRAITS


const int BASIC_RESET = 64;


// Default traits that support all JPEG LS parameters: custom limit, near, maxval (not power of 2)

// This traits class is used to initialize a coder/decoder.
// The coder/decoder also delegates some functions to the traits class.
// This is to allow the traits class to replace the default implementation here with optimized specific implementations.
// This is done for lossless coding/decoding: see losslesstraits.h 

template<typename sample, typename pixel>
struct DefaultTraitsT
{
    typedef sample SAMPLE;
    typedef pixel PIXEL;

    int32_t MAXVAL;
    int32_t RANGE;
    int32_t NEAR;
    int32_t qbpp;
    int32_t bpp;
    int32_t LIMIT;
    int32_t RESET;

    DefaultTraitsT(const DefaultTraitsT& src) :
        MAXVAL(src.MAXVAL),
        RANGE(src.RANGE),
        NEAR(src.NEAR),
        qbpp(src.qbpp),
        bpp(src.bpp),
        LIMIT(src.LIMIT),
        RESET(src.RESET)
    {
    }

    DefaultTraitsT(int32_t max, int32_t jls_near)
    {
        NEAR = jls_near;
        MAXVAL = max;
        RANGE  = (MAXVAL + 2 * NEAR )/(2 * NEAR + 1) + 1;
        bpp = log_2(max);
        LIMIT = 2 * (bpp + MAX(8,bpp));
        qbpp = log_2(RANGE);
        RESET = BASIC_RESET;
    }

    inlinehint int32_t ComputeErrVal(int32_t e) const
    {
        return ModRange(Quantize(e));
    }

    inlinehint SAMPLE ComputeReconstructedSample(int32_t Px, int32_t ErrVal) const
    {
        return FixReconstructedValue(Px + DeQuantize(ErrVal)); 
    }

    inlinehint bool IsNear(int32_t lhs, int32_t rhs) const
    {
        return abs(lhs-rhs) <= NEAR;
    }

    bool IsNear(Triplet<SAMPLE> lhs, Triplet<SAMPLE> rhs) const
    {
        return abs(lhs.v1-rhs.v1) <= NEAR &&
               abs(lhs.v2-rhs.v2) <= NEAR && 
               abs(lhs.v3-rhs.v3) <= NEAR;
    }

    inlinehint int32_t CorrectPrediction(int32_t Pxc) const
    {
        if ((Pxc & MAXVAL) == Pxc)
            return Pxc;

        return (~(Pxc >> (INT32_BITCOUNT-1))) & MAXVAL;
    }

    inlinehint int32_t ModRange(int32_t Errval) const
    {
        ASSERT(abs(Errval) <= RANGE);
        if (Errval < 0)
            Errval = Errval + RANGE;

        if (Errval >= ((RANGE + 1) / 2))
            Errval = Errval - RANGE;

        ASSERT(abs(Errval) <= RANGE/2);

        return Errval;
    }

private:
    int32_t Quantize(int32_t Errval) const
    {
        if (Errval > 0)
            return  (Errval + NEAR) / (2 * NEAR + 1);
        else
            return - (NEAR - Errval) / (2 * NEAR + 1);
    }

    inlinehint int32_t DeQuantize(int32_t Errval) const
    {
        return Errval * (2 * NEAR + 1);
    }

    inlinehint SAMPLE FixReconstructedValue(int32_t val) const
    { 
        if (val < -NEAR)
            val = val + RANGE*(2*NEAR+1);
        else if (val > MAXVAL + NEAR)
            val = val - RANGE*(2*NEAR+1);

        return SAMPLE(CorrectPrediction(val));
    }
};


#endif
