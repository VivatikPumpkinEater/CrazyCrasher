using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creator
{
    public class CCCREATOR : MonoBehaviour
    {
        [SerializeField] private Texture2D CCTEXTURE;
        [SerializeField] private GameObject CCTile;
        [SerializeField] private List<Material> CCBrickMaterials;
        [SerializeField] private VoxSettings CCVoxSettings;

        [SerializeField] private List<CCColors> CCDATAS;
        
        [Serializable]
        public class CCColors
        {
            public Renderer CCRenderer;
            public Color CCColor;
        }

        private void Start()
        {
            foreach (var cc in CCDATAS)
            {
                var newMaterial = new Material(CCBrickMaterials[0])
                {
                    color = cc.CCColor
                };
                    
                cc.CCRenderer.material = newMaterial;
            }
        }
        
        [ContextMenu("Create Prefab")]
        private void CCCreatePrefab()
        {
            CCDATAS.Clear();
            
            var width = CCTEXTURE.width;
            var height = CCTEXTURE.height;

            var cclist = new List<Transform>();
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var pixelColor = CCTEXTURE.GetPixel(x, y);

                    if (pixelColor.a == 0f) // Проверяем прозрачность пикселя
                    {
                        // Создаем пустой объект
                        var emptyObject = Instantiate(CCTile, new Vector3(transform.position.x - x * 0.4f, transform.position.y +y * 0.4f, transform.position.z), Quaternion.identity);
                        emptyObject.transform.parent = transform;
                        emptyObject.transform.localScale = Vector3.one * 40f;
                        var cubeRenderer = emptyObject.GetComponent<Renderer>();
                        cubeRenderer.material = CCBrickMaterials[Random.Range(0, CCBrickMaterials.Count)];
                        cclist.Add(emptyObject.transform);
                        CCDATAS.Add(new CCColors() {CCColor = cubeRenderer.material.color, CCRenderer = cubeRenderer});
                    }
                    else
                    {
                        // Создаем объект-куб с цветом пикселя
                        var cube = Instantiate(CCTile, new Vector3(transform.position.x - x * 0.4f, transform.position.y +y * 0.4f, transform.position.z), Quaternion.identity);
                        var cubeRenderer = cube.GetComponent<Renderer>();
                        var newMaterial = Instantiate(cubeRenderer.material);
                        newMaterial.color = pixelColor;
                        cubeRenderer.material = newMaterial;
                        CCDATAS.Add(new CCColors() {CCColor = pixelColor, CCRenderer = cubeRenderer});
                        cube.transform.parent = transform;
                        cube.transform.localScale = Vector3.one * 40f;
                        cclist.Add(cube.transform);
                    }
                }
            }

            CCVoxSettings._micros = cclist;
            CCVoxSettings.RandomizeRotation();
        }
    }
}