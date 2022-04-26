using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField] CelestialBody[] satellites;
    [SerializeField] Light sunLight;

    [SerializeField][Range(0.1f, 1f)] float diameterScale = 1f;
    [SerializeField][Range(0.0005f, 1f)] float distanceScale = 0.0005f;

    [SerializeField][Range(0, 1)] float normalizeDistanceStrength = 1f;
    [SerializeField][Range(0, 1)] float normalizeDiameterStrength = 1f;

    [SerializeField] float[] distancesActual;
    [SerializeField] float[] diametersActual;
    [SerializeField] float[] distancesScaled;
    [SerializeField] float[] diametersScaled;

    const float DiameterToDistance = 11544.96f;

    void Start()
    {
        SetActualDistancesAndSizes();
    }
    
    void Update()
    {
        SetScaledDistancesAndSizes();
        UpdatePositionsAndScale();
    }
    

    void SetActualDistancesAndSizes()
    {
        distancesActual = new float[satellites.Length];
        diametersActual = new float[satellites.Length];
        
        for (int i = 0; i < satellites.Length; i++)
            distancesActual[i] = satellites[i].semimajorAxis;
        
        for (int i = 0; i < satellites.Length; i++)
            diametersActual[i] = satellites[i].diameter;
    }

    void SetScaledDistancesAndSizes()
    {
        sunLight.range = DiameterToDistance * distanceScale * 40;
        distancesScaled = RescaleArray(distancesActual, normalizeDistanceStrength, distanceScale * DiameterToDistance);
        diametersScaled = AverageArray(diametersActual, normalizeDiameterStrength, diameterScale);
    }

    float[] RescaleArray(float[] values, float strength, float scale)
    {
        float minValue = values.Min() * scale;
        float maxValue = values.Max() * scale;
        float[] normalizedArray = NormalizeArray(values, minValue, maxValue);
        float[] flatArray = FlattenArray(values, minValue, maxValue);
        float[] scaledArray = new float[values.Length];

        for (int i = 0; i < values.Length; i++)
            scaledArray[i] = normalizedArray[i] * (1 - strength) + flatArray[i] * strength;
        return scaledArray;
    }

    float[] NormalizeArray(float[] values, float minValue, float maxValue)
    {
        float[] normalizedArray = new float[values.Length];
        float diff = maxValue - minValue;
        float diffValues = values.Max() - values.Min();

        for (int i = 0; i < normalizedArray.Length; i++)
            normalizedArray[i] = (values[i] - values.Min()) * diff / diffValues + minValue;
        return normalizedArray;
    }

    float[] FlattenArray(float[] values, float minValue, float maxValue)
    {
        float[] flattenedArray = new float[values.Length];
        List<float> sequence = new List<float>(values);
        float diff = (maxValue - minValue) / (values.Length - 1);
        
        sequence.Sort();

        for (int i = 0; i < values.Length; i++)
            flattenedArray[i] = minValue + sequence.IndexOf(values[i]) * diff;

        return flattenedArray;
    }

    float[] AverageArray(float[] values, float strength, float scale)
    {
        float[] averagedArray = new float[values.Length];
        float arrayMean = values.Sum() / values.Length;
        
        for (int i = 0; i < values.Length; i++)
            averagedArray[i] = (arrayMean * strength + values[i] * (1 - strength)) * scale;
        return averagedArray;
    }
    
    void UpdatePositionsAndScale()
    {
        for (int i = 0; i < distancesScaled.Length; i++)
        {
            satellites[i].transform.position = new Vector3(
                distancesScaled[i], 
                0, 
                0);
            satellites[i].transform.localScale = new Vector3(
                diametersScaled[i],
                diametersScaled[i], 
                diametersScaled[i]);
        }
    }
}
