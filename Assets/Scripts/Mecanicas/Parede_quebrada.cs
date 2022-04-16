using System.Collections;
using System.Collections.Generic;
using UnityEngine;




    public class Parede_quebrada : MonoBehaviour
    {
        public GameObject parede;
        public GameObject paredeQuebrada;
        public GameObject particulaPoera;
        public Collider _collider;
        //public Outline outlineParede1;
        //public Outline outlineParede2;

        [Space]
        [Space]

        public float tempoParaQuebrar;
        public float tempoParaSumir;

        [Space]
        [Space]

        public Transform centroExplosao;
        public float forcaExplosao;
        public float explosaoRadio;

        public static string nome;
        public static bool comecaQuebrar = false;

        int contador = 2;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }




        private void OnCollisionStay(Collision collision)
        {

            
            if (collision.gameObject.tag == "Player_1" && comecaQuebrar == true)
            {
                print("é ele");
                StartCoroutine(QuebrarParede());
                comecaQuebrar = false;


            }
        }


        IEnumerator QuebrarParede()
        {
            
            if (contador > 0)
             {
                Instantiate(particulaPoera, new Vector3(-0.05f, 3f, -1.67f), Quaternion.Euler(0f, 180, 0f));
                contador--;
             }

            yield return new WaitForSeconds(tempoParaQuebrar);

            //outlineParede1.eraseRenderer = true;
            //outlineParede2.eraseRenderer = true;

            
            parede.SetActive(false);
            paredeQuebrada.SetActive(true);

            for (int i = 0; i < paredeQuebrada.transform.childCount; i++)
            {
                GameObject child = paredeQuebrada.transform.GetChild(i).gameObject;

                child.GetComponent<Rigidbody>().AddExplosionForce(forcaExplosao, centroExplosao.position, explosaoRadio);

            }

            _collider.enabled = false;


            Destroy(paredeQuebrada, tempoParaSumir);

        }
    }

