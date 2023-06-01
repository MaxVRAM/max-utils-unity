using UnityEngine;

namespace MaxVram.Modules
{
    public static class MaxBalls
    {
        public static void SphericalToCartesian(float radius, float polar, float elevation, out Vector3 cartesianCoords)
        {
            // https://blog.nobel-joergensen.com/2010/10/22/spherical-coordinates-in-unity/
            float a = radius * Mathf.Cos(elevation);
            cartesianCoords.x = a * Mathf.Cos(polar);
            cartesianCoords.y = radius * Mathf.Sin(elevation);
            cartesianCoords.z = a * Mathf.Sin(polar);
        }
        
        public static Vector3 SphericalToCartesian(float radius, float polar, float elevation)
        {
            SphericalToCartesian(radius, polar, elevation, out Vector3 outCart);
            return outCart;
        }
        
        public static Vector3 SphericalToCartesian(SphericalCoordinates sphericalCoordinates)
        {
            SphericalToCartesian(sphericalCoordinates.Radius, sphericalCoordinates.Polar, sphericalCoordinates.Elevation, out Vector3 outCart);
            return outCart;
        }

        public static void CartesianToSpherical(Vector3 cartesianCoords, out float outRadius, out float outPolar, out float outElevation)
        {
            // https://blog.nobel-joergensen.com/2010/10/22/spherical-coordinates-in-unity/
            if (cartesianCoords.x == 0)
                cartesianCoords.x = Mathf.Epsilon;
            outRadius = Mathf.Sqrt((cartesianCoords.x * cartesianCoords.x)
                            + (cartesianCoords.y * cartesianCoords.y)
                            + (cartesianCoords.z * cartesianCoords.z));
            outPolar = Mathf.Atan(cartesianCoords.z / cartesianCoords.x);
            if (cartesianCoords.x < 0)
                outPolar += Mathf.PI;
            outElevation = Mathf.Asin(cartesianCoords.y / outRadius);
        }
        
        public static SphericalCoordinates CartesianToSpherical(Vector3 cartesianCoords)
        {
            CartesianToSpherical(cartesianCoords, out float outRadius, out float outPolar, out float outElevation);
            return new SphericalCoordinates(outRadius, outPolar, outElevation);
        }

        public struct SphericalCoordinates
        {
            private readonly float _radius;
            private readonly float _polar;
            private readonly float _elevation;
            public float Radius => _radius;
            public float Polar => _polar;
            public float Elevation => _elevation;

            public SphericalCoordinates(float radius, float polar, float elevation)
            {
                _radius = radius;
                _polar = polar;
                _elevation = elevation;
            }
            
            public SphericalCoordinates(Vector3 cartesianCoords)
            {
                CartesianToSpherical(cartesianCoords, out _radius, out _polar, out _elevation);
            }
            
            public Vector3 ToCartesian()
            {
                SphericalToCartesian(_radius, _polar, _elevation, out Vector3 cartesianCoords);
                return cartesianCoords;
            }
        }
    }
}