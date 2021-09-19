using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Data
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Mesh instanceMesh;
        [SerializeField] private Material instanceMaterial;
        [SerializeField] private int instanceCountAxis = 10;
        
        [SerializeField] private int timeSteps = 100;
        [SerializeField] private int timeStepsPerSecond = 25;

        private Matrix4x4[][] _matrices;
        private Matrix4x4[][][] _scaling;

        private float _timeInProcess = 0F;
        private float _maxTime;

        private bool _isPlaying = false;

        // TODO: Clean this prototype mess.
        private void Awake()
        {
            int nInstances = instanceCountAxis * instanceCountAxis;
            int nArrays = nInstances / 1023 + 1;

            _matrices = new Matrix4x4[nArrays][];
            _scaling = new Matrix4x4[nArrays][][];

            int currMatrixArray = 0;
            int nMatricesInArray = 0;

            float stepSize = 4F / instanceCountAxis;
            Vector3 scale = new Vector3(2F / instanceCountAxis, 0.1F, 2F / instanceCountAxis);
            
            for (var j = 0; j < instanceCountAxis; j++)
            for (var i = 0; i < instanceCountAxis; i++)
            {
                if (nMatricesInArray == 1023)
                {
                    nMatricesInArray = 0;
                    currMatrixArray += 1;
                }
                if (nMatricesInArray == 0)
                {
                    int nArraySize = System.Math.Min(1023, nInstances - (currMatrixArray) * 1023);
                    _matrices[currMatrixArray] = new Matrix4x4[nArraySize];
                    _scaling[currMatrixArray] = new Matrix4x4[nArraySize][];
                }

                float posX = stepSize * i - 2F;
                float posZ = stepSize * j - 2F;
                _matrices[currMatrixArray][nMatricesInArray] = 
                    Matrix4x4.Translate(new Vector3(posX, 0F, posZ)) *
                    Matrix4x4.Scale(scale);

                _scaling[currMatrixArray][nMatricesInArray] =
                    Enumerable.Range(0, timeSteps)
                        .Select(v => v * 2F * Mathf.PI / timeSteps)
                        .Select(v => 3F + Mathf.Sin((v - 2F * Mathf.PI * i / instanceCountAxis)) + Mathf.Sin((v - 2F * Mathf.PI * j / instanceCountAxis)))
                        .Select(v => Matrix4x4.Scale(new Vector3(1F, v, 1F)))
                        .ToArray();

                nMatricesInArray += 1;
            }

            _maxTime = (float) timeSteps / (float) timeStepsPerSecond;
        }

        private void Update()
        {
            if (_isPlaying)
            {
                _timeInProcess += Time.deltaTime;
                _timeInProcess %= _maxTime;
            }

            int timeStep = Mathf.FloorToInt(_timeInProcess * timeStepsPerSecond);

            foreach (var (dat, sca) in _matrices.Zip(_scaling,
                (dat, sca) => new Tuple<Matrix4x4[], Matrix4x4[][]>(dat, sca)))
            {
                var arr = dat.Zip(sca, (v, s) => s[timeStep] * v).ToArray();
                Graphics.DrawMeshInstanced(instanceMesh, 0, instanceMaterial, arr);
            }
        }
        
        [DllImport("__Internal")]
        private static extern void ToggledPlaying();

        public void TogglePlaying()
        {
            _isPlaying = !_isPlaying;
            
#if (UNITY_WEBGL && !UNITY_EDITOR)
        ToggledPlaying();
#endif
        } 
    }
}
