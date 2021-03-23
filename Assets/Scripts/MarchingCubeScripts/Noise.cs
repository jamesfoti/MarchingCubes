using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoiseTest;

public static class Noise {

    private static OpenSimplexNoise noise = new OpenSimplexNoise();

    public static float PerlinNoise3D(float x, float y, float z) {
        // Taken from Omar Santiago on https://www.youtube.com/watch?v=JdeyNbDACV0&t=1587s
        float AB = Mathf.PerlinNoise(x, y);
        float BC = Mathf.PerlinNoise(y, z);
        float AC = Mathf.PerlinNoise(x, z);

        float BA = Mathf.PerlinNoise(y, x);
        float CB = Mathf.PerlinNoise(z, y);
        float CA = Mathf.PerlinNoise(z, x);

        float ABC = AB + BC + AC + BA + CB + CA;
        return (ABC / 6f);
    }

    public static float SimplexNoise(float x, float y, float z) {
        return (float) noise.Evaluate(x, y, z);
    }
}
