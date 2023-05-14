using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyline : MonoBehaviour
{
    [SerializeField] float parallax;//Offset para o movemento da textura.

    Material mat;//Variable de tipo Material para obter referencia á textura do fondo.

    //Variable tipo Transform para obter a referencia ó Transform da cámara e polo tanto á súa posición.
    Transform cam;
   
    Vector3 initialPos;//Variable para obter a posición inicial do noso GameObject (Imaxes do fondo).

    void Start()
    {
        //Obtemos referencia ó material do noso GameObject(Imaxes do fondo).
        mat = GetComponent<SpriteRenderer>().material;

        cam = Camera.main.transform;//Referencia ó Transform da cámara principal.

        initialPos = transform.position;//Posición inicial do noso GameObject (Imaxes do fondo).
    }

    // Update is called once per frame
    void Update()
    {
        //facemos que o noso GameObject (Imaxes do fondo) se mova só en X coa cámara.
        transform.position = new Vector3 (cam.position.x, initialPos.y, initialPos.z);

        //facemos que oa textura do noso GameObject (Imaxes do fondo) se mova só en X coa cámara
        //pero aplicando un offset, que será diferente a cada unha das texturas das dúas imaxes de fondo.
        mat.mainTextureOffset = new Vector2 (cam.position.x * parallax, 0);
    }
}
