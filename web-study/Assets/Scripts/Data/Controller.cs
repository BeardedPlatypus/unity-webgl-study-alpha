using UnityEngine;

namespace Data
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Mesh instanceMesh;
        [SerializeField] private Material instanceMaterial;
        [SerializeField] private int instanceCountAxis = 10;
        
        private Matrix4x4[][] _matrices;

        private void Awake()
        {
            int nInstances = instanceCountAxis * instanceCountAxis;
            int nArrays = nInstances / 1023 + 1;

            _matrices = new Matrix4x4[nArrays][];

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
                }

                float posX = stepSize * i - 2F;
                float posZ = stepSize * j - 2F;
                _matrices[currMatrixArray][nMatricesInArray] = 
                    Matrix4x4.Translate(new Vector3(posX, 0F, posZ)) *
                    Matrix4x4.Scale(scale);

                nMatricesInArray += 1;
            }
        }

        private void Update() {
            foreach (var arr in _matrices)
                Graphics.DrawMeshInstanced(instanceMesh, 0, instanceMaterial, arr);
        }

    }
}
