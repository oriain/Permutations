using System;

namespace Permutations
{
    class Program
    {
        static void Main()
        {
            char[] array = {'A', 'B', 'C', 'D'};
            Console.WriteLine("Source: {0}", String.Join(", ", array));

            int[] mask = {1, 2, 3, 0};
            mask = new[] {2, 1, 3, 0};
            Console.WriteLine("Mask: {0}", String.Join(", ", mask));

            bool isValid = ValidateMask(array.Length, mask);
            if (!isValid) Console.WriteLine("The mask is not valid.");
            else
            {
                ApplyPermutation(array, mask);
                Console.WriteLine("Permutaton: {0}", String.Join(", ", array));

                InvertMask(mask);
                Console.WriteLine("Inverted Mask: {0}", String.Join(", ", mask));

                isValid = ValidateMask(array.Length, mask);
                if (!isValid) Console.WriteLine("The inverted mask is not valid.");
                else
                {
                    ApplyPermutation(array, mask);
                    Console.WriteLine("Inversion: {0}", String.Join(", ", array));
                } // End of inverted mask validation
            } // End of original mask validation

            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        static bool ValidateMask(int n, int[] mask)
        {
            // The mask must account for all elements of the
            // original array, not any more or less.
            if (n != mask.Length) return false;

            int[] copy = new int[n];
            Array.Copy(mask, copy, n);
            Array.Sort(copy);

            // The values of the array should be all the indices too.
            for (int i = 0; i < copy.Length; i++)
            {
                if (i != copy[i]) return false;
            }

            // If it passes the length and values validations, the permuation must be valid.
            return true;
        }

        static void ApplyPermutation(char[] array, int[] mask)
        {
            // We need to make a copy of the mask so that we don't
            // destroy the original during permutation process.
            int[] m = new int[mask.Length];
            for (int i = 0; i < mask.Length; i++)
            {
                m[i] = mask[i];
            }

            //Console.WriteLine("Before: {0}", String.Join(", ", array));
            //Console.WriteLine("Before: {0}", String.Join(", ", mask));
            for (int i = 0; i < array.Length; i++)
            {
                while (i != m[i])
                {
                    // Swap the actual characters.
                    char swap = array[m[i]];
                    array[m[i]] = array[i];
                    array[i] = swap;

                    // Also update the mask.
                    int maskSwap = m[m[i]];
                    m[m[i]] = m[i];
                    m[i] = maskSwap;

                    //Console.WriteLine("While-Loop: {0}", String.Join(", ", array));
                    //Console.WriteLine("While-Loop: {0}", String.Join(", ", mask));
                }
            }
            //Console.WriteLine("After:  {0}", String.Join(", ", array));
            //Console.WriteLine("After:  {0}", String.Join(", ", mask));
        }

        static void InvertMaskStepThrough(int[] mask)
        {
            // 1, 2, 3, 0

            int i = 0;
            int swap = mask[mask[i]]; //    i == 0, mask[0] == 1, mask[1] == 2,    swap = 2
            mask[mask[i]] = i;        //    i == 0, mask[0] == 1,               mask[1] = 0
            mask[i] = swap;           // swap == 2,       i == 0,               mask[0] = 2

            // 2, 0, 3, 0

            i = 1;
            swap = mask[mask[i]]; //    i == 1, mask[1] == 0, mask[0] == 2,    swap = 2
            mask[mask[i]] = i;    //    i == 1, mask[1] == 0,               mask[0] = 1
            mask[i] = swap;       // swap == 2,       i == 1,               mask[1] = 2

            // 1, 2, 3, 0

            i = 2;
            swap = mask[mask[i]]; //    i == 2, mask[2] == 3, mask[3] == 0,    swap = 0
            mask[mask[i]] = i;    //    i == 2, mask[2] == 3,               mask[3] = 2
            mask[i] = swap;       // swap == 0,       i == 2,               mask[2] = 0

            // 1, 2, 0, 2

            i = 3;
            swap = mask[mask[i]]; //    i == 3, mask[3] == 2, mask[2] == 0,    swap = 0
            mask[mask[i]] = i;    //    i == 3, mask[3] == 2,               mask[2] = 3
            mask[i] = swap;       // swap == 0,       i == 3,               mask[3] = 0

            // 1, 2, 3, 0

            // A, B, C, D
            // 1, 2, 3, 0
            // D, A, B, C
            // 1, 2, 3, 0
            // C, D, A, B
        }

        static void InvertMaskBrute(int[] mask)
        {
            // We need to make a correction for each element in the mask.
            int numOfCorrections = 0;

            // Start with item 0.
            int i = 0;
            
            // Keep looping until we fix each element.
            while (numOfCorrections < mask.Length)
            {
                int j = 0;
                while (j < mask.Length && mask[j] != i) j++;
                if (j >= mask.Length || mask[j] != i) continue;
                int temp = mask[i];
                mask[i] = j;
                i = temp;
                numOfCorrections++;
            }
            
        }

        static void InvertMask(int[] mask)
        {
            //Console.WriteLine("Original mask: {0}", String.Join(", ", mask));

            int i = 0;
            int j = mask[i];

            for (int k = 0; k < mask.Length; k++)
            {
                int tmp = mask[j];
                mask[j] = i;
                i = j;
                j = tmp;

                //Console.WriteLine("Pass {0}: {1}", k, String.Join(", ", mask));
            }
        }
    }
}
