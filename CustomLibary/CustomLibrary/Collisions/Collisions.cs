using UnityEngine;

namespace CustomLibrary.Collisions
{
    public class GoodCollisions : MonoBehaviour
    {

        /// <summary>
        /// Controleer of er iets in de weg staat met een automatische korte distance (0.05f)
        /// </summary>
        /// <param name="thisComponent">dit object</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(Component thisComponent, Vector2 direction)
        {
            GameObject gameObject = thisComponent.gameObject;

            Vector2 sideOffset = CalcSideOffset(direction);
            float _distance = CalcDistance(sideOffset, gameObject.GetComponent<BoxCollider2D>()); 

            return PerformRaycasts(gameObject.transform.position, direction, sideOffset, _distance, new string[1]);
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde tag met een automatische korte distance (0.05)
        /// </summary>
        /// <param name="thisComponent">dit object</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="withTag">Welk tag het obstakel moet hebben, dit mogen er meerdere zijn</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(Component thisComponent, Vector2 direction, string[] withTag)
        {
            GameObject gameObject = thisComponent.gameObject;

            Vector2 sideOffset = CalcSideOffset(direction);
            float _distance = CalcDistance(sideOffset, gameObject.GetComponent<BoxCollider2D>());

            return PerformRaycasts(gameObject.transform.position, direction, sideOffset, _distance, withTag);
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde tag met een automatische korte distance (0.05)
        /// </summary>
        /// <param name="thisComponent">dit object</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="withTag">Welk tag het obstakel moet hebben</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(Vector3 origin,Component thisComponent, Vector2 direction, string[] withTag)
        {
            GameObject gameObject = thisComponent.gameObject;

            Vector2 sideOffset = CalcSideOffset(direction);
            float _distance = CalcDistance(sideOffset, gameObject.GetComponent<BoxCollider2D>());

            return PerformRaycasts(origin, direction, sideOffset, _distance, withTag);
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde tag met een automatische korte distance (0.05)
        /// </summary>
        /// <param name="thisComponent">dit object</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="withTag">Welk tag het obstakel moet hebben</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(Component thisComponent, Vector2 direction,float distance, string[] withTag)
        {
            GameObject gameObject = thisComponent.gameObject;

            Vector2 sideOffset = CalcSideOffset(direction);
            float _distance = distance;

            return PerformRaycasts(gameObject.transform.position, direction, sideOffset, _distance, withTag);
        }

        private static bool PerformRaycasts(Vector3 origin, Vector2 direction, Vector2 sideoffset, float distance, string[] tags)
        {
            Color drawColor = new Color(0, 1, 0);

            RaycastHit2D hit;
            bool hitSomething =
           ((hit = Physics2D.Raycast((Vector2)origin, direction, distance + 0.05f)) ||
            (hit = Physics2D.Raycast((Vector2)origin + sideoffset * distance, direction, distance + 0.05f )) ||
            (hit = Physics2D.Raycast((Vector2)origin + -sideoffset * distance, direction, distance + 0.05f )));

            bool hasTag = false;
            // op het moment dat we iets raken
            if (hitSomething) {
                // checken we voor alle tags die zijn meegegeven 
                foreach (string tag in tags) {

                    // of de tag niet leeg is, zo ja breek de loop
                    if (string.IsNullOrEmpty(tag)) {
                        break;
                    }
                        
                    //anders check je of de collider deze specifieke tag heeft
                    if (hit.collider.CompareTag(tag)) {
                        hasTag = true;
                        break;
                    }    
                }
                drawColor = new Color(1, 0, 0); //Rays rood tekenen
            }

            Debug.DrawRay(origin, direction * distance, drawColor);
            Debug.DrawRay((Vector2)origin + (sideoffset * distance), direction * (distance + 0.05f), drawColor);
            Debug.DrawRay((Vector2)origin + (-sideoffset * distance), direction * (distance + 0.05f), drawColor);

            return string.IsNullOrEmpty(tags[0]) || hasTag;
        }

        private static Vector2 CalcSideOffset(Vector2 direction)
        {
            if (direction == Vector2.left || direction == Vector2.right) return Vector2.up;
            return Vector2.left;
        }

        private static float CalcDistance(Vector2 sideOffset, BoxCollider2D collider)
        {
            if (sideOffset == Vector2.up)
                return (collider.size.y / 2);

            return (collider.size.x / 2);
        }

    }
}      