﻿using UnityEngine;

namespace CustomLibrary.Collisions {
    public class GoodCollisions : MonoBehaviour {

        /// <summary>
        /// Controleer of er iets in de weg staat met een automatische korte distance (0.05f)
        /// </summary>
        /// <param name="gameObject">Uw gameobject</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(Component thisComponent, Vector2 direction)
        {
            GameObject gameObject = thisComponent.gameObject;

            Vector2 sideoffset;
            if (direction == Vector2.left || direction == Vector2.right) sideoffset = Vector3.up;
            else sideoffset = Vector3.left;

            float _distance;
            if (sideoffset == Vector2.up) _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;
            else _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;

            RaycastHit2D hit = PerformRaycasts(gameObject.transform.position, direction, sideoffset, _distance);

            if (hit) //Iets geraakt
                return true;

            return false; //Niets geraakt
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een automatische korte distance (0.05f)
        /// </summary>
        /// <param name="gameObject">Uw gameobject</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(GameObject gameObject, Vector2 direction)
        {
            Vector2 sideoffset;
            if (direction == Vector2.left || direction == Vector2.right) sideoffset = Vector3.up;
            else sideoffset = Vector3.left;

            float _distance;
            if (sideoffset == Vector2.up) _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;
            else _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;

            RaycastHit2D hit = PerformRaycasts(gameObject.transform.position, direction, sideoffset, _distance);

            if (hit) //Iets geraakt
                return true;

            return false; //Niets geraakt
        }

        /// <summary>
        /// Controleer of er iets in de weg staat
        /// </summary>
        /// <param name="gameObject">Uw gameobject</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="distance">Hoe ver we vanaf buiten de Box Collider checken</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(GameObject gameObject, Vector2 direction, float distance) {
            Vector2 sideoffset;
            if (direction == Vector2.left || direction == Vector2.right) sideoffset = Vector3.up;
            else sideoffset = Vector3.left;

            float _distance;
            if (sideoffset == Vector2.up) _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + distance;
            else _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + distance;

            RaycastHit2D hit = PerformRaycasts(gameObject.transform.position, direction, sideoffset, _distance);

            if (hit) //Iets geraakt
                return true;

            return false; //Niets geraakt
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde tag
        /// </summary>
        /// <param name="gameObject">Uw gameobject</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="distance">Hoe ver we vanaf buiten de Box Collider checken</param>
        /// <param name="withTag">Welk tag het obstakel moet hebben</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(GameObject gameObject, Vector2 direction, float distance, string withTag) {
            Vector2 sideoffset;
            if (direction == Vector2.left || direction == Vector2.right) sideoffset = Vector3.up;
            else sideoffset = Vector3.left;

            float _distance;
            if (sideoffset == Vector2.up) _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + distance;
            else _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + distance;

            RaycastHit2D hit = PerformRaycasts(gameObject.transform.position, direction, sideoffset, _distance);

            if (hit && hit.collider.CompareTag(withTag)) //Iets geraakt met tag
                return true;

            return false; //Niets geraakt met tag
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde tag met een automatische korte distance (0.05)
        /// </summary>
        /// <param name="gameObject">Uw gameobject</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="withTag">Welk tag het obstakel moet hebben</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(GameObject gameObject, Vector2 direction, string withTag)
        {
            Vector2 sideoffset;
            if (direction == Vector2.left || direction == Vector2.right) sideoffset = Vector3.up;
            else sideoffset = Vector3.left;

            float _distance;
            if (sideoffset == Vector2.up) _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;
            else _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;

            RaycastHit2D hit = PerformRaycasts(gameObject.transform.position, direction, sideoffset, _distance);

            if (hit && hit.collider.CompareTag(withTag)) //Iets geraakt met tag
                return true;

            return false; //Niets geraakt met tag
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde layer
        /// </summary>
        /// <param name="gameObject">Uw gameobject</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="distance">Hoe ver we vanaf buiten de Box Collider checken</param>
        /// <param name="layerMask">De layer waarin we controleren</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(GameObject gameObject, Vector2 direction, float distance, int layerMask)
        {
            Vector2 sideoffset;
            if (direction == Vector2.left || direction == Vector2.right) sideoffset = Vector2.up;
            else sideoffset = Vector2.left;

            Color drawColor = Color.magenta;

            Vector3 origin = gameObject.transform.position;
            float _distance;
            if (sideoffset == Vector2.up) _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + distance;
            else _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + distance;

            RaycastHit2D hit;
            bool hitSomething =
           ((hit = Physics2D.Raycast((Vector2)origin, direction, _distance, layerMask)) ||
            (hit = Physics2D.Raycast((Vector2)origin + sideoffset * 0.5f, direction, _distance, layerMask)) ||
            (hit = Physics2D.Raycast((Vector2)origin + -sideoffset * 0.5f, direction, _distance, layerMask)));

            if (hitSomething)
                drawColor = Color.yellow;

            Debug.DrawRay(origin, direction * _distance, drawColor);
            Debug.DrawRay((Vector2)origin + sideoffset * 0.5f, direction * _distance, drawColor);
            Debug.DrawRay((Vector2)origin + -sideoffset * 0.5f, direction * _distance, drawColor);

            if (hit) //Iets geraakt met de opgegeven layer
                return true;

            return false; //Niets geraakt met de opgegeven layer
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde layer met een automatische korte distance (0.05)
        /// </summary>
        /// <param name="gameObject">Uw gameobject</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="layerMask">De layer waarin we controleren</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(GameObject gameObject, Vector2 direction, int layerMask)
        {
            Vector2 sideoffset;
            if (direction == Vector2.left || direction == Vector2.right) sideoffset = Vector2.up;
            else sideoffset = Vector2.left;

            Color drawColor = Color.magenta;

            Vector3 origin = gameObject.transform.position;
            float _distance;
            if (sideoffset == Vector2.up) _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;
            else _distance = (gameObject.GetComponent<BoxCollider2D>().size.x / 2) + 0.05f;

            RaycastHit2D hit;
            bool hitSomething =
           ((hit = Physics2D.Raycast((Vector2)origin, direction, _distance, layerMask)) ||
            (hit = Physics2D.Raycast((Vector2)origin + sideoffset * 0.5f, direction, _distance, layerMask)) ||
            (hit = Physics2D.Raycast((Vector2)origin + -sideoffset * 0.5f, direction, _distance, layerMask)));

            if (hitSomething)
                drawColor = Color.yellow;

            Debug.DrawRay(origin, direction * _distance, drawColor);
            Debug.DrawRay((Vector2)origin + sideoffset * 0.5f, direction * _distance, drawColor);
            Debug.DrawRay((Vector2)origin + -sideoffset * 0.5f, direction * _distance, drawColor);

            if (hit) //Iets geraakt met de opgegeven layer
                return true;

            return false; //Niets geraakt met de opgegeven layer
        }

        private static RaycastHit2D PerformRaycasts(Vector3 origin, Vector2 direction, Vector2 sideoffset, float distance) {
            Color drawColor = new Color(0, 1, 0);

            RaycastHit2D hit;
            bool hitSomething =
           ((hit = Physics2D.Raycast((Vector2)origin, direction, distance)) ||
            (hit = Physics2D.Raycast((Vector2)origin + sideoffset * 0.5f, direction, distance)) ||
            (hit = Physics2D.Raycast((Vector2)origin + -sideoffset * 0.5f, direction, distance)));

            if (hitSomething)
                drawColor = new Color(1, 0, 0); //Rays rood tekenen

            Debug.DrawRay(origin , direction * distance, drawColor);
            Debug.DrawRay((Vector2)origin +  sideoffset * 0.5f, direction * distance, drawColor);
            Debug.DrawRay((Vector2)origin + -sideoffset * 0.5f, direction * distance, drawColor);

            return hit;
        }

		//------------------------------------
        //AVG versie
        //------------------------------------


        public static bool CheckSideV2(GameObject gameObject, Vector2 direction, float distance, int layerMask, bool drawRays = true) {
            BoxCollider2D boxColl = gameObject.GetComponent<BoxCollider2D>();
            Vector2 sideoffset = (direction.y == 0) ? Vector2.up : Vector2.left;

            Vector3 origin = gameObject.transform.position;

            distance += GetAxisSizeFromCollBox(boxColl, (sideoffset == Vector2.up)? false : true,0.5f);

            bool hitSomething = HitSomething(origin,direction,distance, sideoffset,  layerMask);

            if (drawRays) {
                DrawRays(origin, direction, sideoffset, distance, (hitSomething) ? Color.yellow : Color.magenta);
            }

            return hitSomething;
        }

        static void DrawRays(Vector2 origin, Vector2 direction, Vector2 sideoffset, float _distance, Color drawColor) {
            Debug.DrawRay(origin, direction * _distance, drawColor);                                // midden
            Debug.DrawRay((Vector2)origin + sideoffset * 0.5f, direction * _distance, drawColor);   // boven/rechts
            Debug.DrawRay((Vector2)origin + -sideoffset * 0.5f, direction * _distance, drawColor);  // beneden/links
        }

        static bool HitSomething(Vector2 origin, Vector2 direction, float _distance, Vector2 sideoffset, int layerMask = -1) {
            return
            ((Physics2D.Raycast((Vector2)origin, direction, _distance, layerMask)) ||                     // midden
            (Physics2D.Raycast((Vector2)origin + sideoffset * 0.5f, direction, _distance, layerMask)) ||  // boven/rechts
            (Physics2D.Raycast((Vector2)origin + -sideoffset * 0.5f, direction, _distance, layerMask)));  // beneden/links             
        }
        static float GetAxisSizeFromCollBox(BoxCollider2D coll,bool verticalAxis, float multiplier = 1) {
            if (coll==null) {
                return 1 * multiplier;
            }
            else if (!verticalAxis) {
                return coll.size.x * multiplier;
            }
            else {
                return coll.size.y * multiplier;
            }
        }

        public static bool CheckSideV2(GameObject gameObject, Vector2 direction, bool drawRays = true) {
            return CheckSideV2(gameObject,direction,0.55f,-1,drawRays); //layermask met een waarde van -1 betekent 'everything = true'
        }
        public static bool CheckSideV2(Component comp, Vector2 direction, bool drawRays = true) {
            return CheckSideV2(comp.gameObject, direction, 0.55f, -1,drawRays); //layermask met een waarde van -1 betekent 'everything = true'
        }

        //------------------------------------
        //------------------------------------
    }

}
