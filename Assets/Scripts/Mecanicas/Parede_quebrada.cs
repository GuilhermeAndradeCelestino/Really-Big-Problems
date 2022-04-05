using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace cakeslice
{
    public class Parede_quebrada : MonoBehaviour
    {
        public GameObject parede;
        public GameObject paredeQuebrada;
        public Collider _collider;



        public Outline outlineParede1;
        public Outline outlineParede2;

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

            
            if (collision.gameObject.tag == "Player_1" && ScriptPlayer1.interactP1 == true)
            {
                print("é ele");
                StartCoroutine(QuebrarParede());
            }
        }


        IEnumerator QuebrarParede()
        {
            yield return new WaitForSeconds(tempoParaQuebrar);

            outlineParede1.eraseRenderer = true;
            outlineParede2.eraseRenderer = true;



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

}