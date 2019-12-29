using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ErksUnityLibrary.HexMap
{
    public class HexUnit : MonoBehaviour
    {
        public static HexUnit unitPrefab;

        private const float travelSpeed = 4f;
        private const float rotationSpeed = 180f;
        public int VisionRange
        {
            get
            {
                return 3;
            }
        }

        private List<HexCell> pathToTravel;
        private HexCell location, currentTravelLocation;

        public HexGrid Grid { get; set; }

        public HexCell Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location)
                {
                    Grid.DecreaseVisibility(location, VisionRange);
                    location.Unit = null;
                }
                location = value;
                value.Unit = this;
                Grid.IncreaseVisibility(value, VisionRange);
                transform.localPosition = value.Position;
            }
        }

        public float Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                orientation = value;
                transform.localRotation = Quaternion.Euler(0f, value, 0f);
            }
        }

        public int Speed
        {
            get
            {
                return 24;
            }
        }

        private float orientation;

        public void ValidateLocation()
        {
            transform.localPosition = location.Position;
        }

        public bool IsValidDestination(HexCell cell)
        {
            return cell.IsExplored && !cell.IsUnderwater && !cell.Unit;
        }

        public void Travel(List<HexCell> path)
        {
            location.Unit = null;
            location = path[path.Count - 1];
            location.Unit = this;
            pathToTravel = path;
            StopAllCoroutines();
            StartCoroutine(TravelPath());
        }

        private IEnumerator TravelPath()
        {
            Vector3 a, b, c = pathToTravel[0].Position;
            yield return LookAt(pathToTravel[1].Position);
            Grid.DecreaseVisibility(currentTravelLocation ? currentTravelLocation : pathToTravel[0], VisionRange);

            float t = Time.deltaTime * travelSpeed;
            for (int i = 1; i < pathToTravel.Count; i++)
            {
                currentTravelLocation = pathToTravel[i];
                a = c;
                b = pathToTravel[i - 1].Position;
                c = (b + currentTravelLocation.Position) * 0.5f;

                Grid.IncreaseVisibility(pathToTravel[i], VisionRange);

                for (; t < 1f; t += Time.deltaTime * travelSpeed)
                {
                    transform.localPosition = Bezier.GetPoint(a, b, c, t);
                    Vector3 d = Bezier.GetDerivative(a, b, c, t);
                    d.y = 0f;
                    transform.localRotation = Quaternion.LookRotation(d);
                    yield return null;
                }

                Grid.DecreaseVisibility(pathToTravel[i], VisionRange);

                t -= 1f;
            }

            currentTravelLocation = null;

            a = c;
            b = location.Position;
            c = b;

            Grid.IncreaseVisibility(location, VisionRange);

            for (; t < 1f; t += Time.deltaTime * travelSpeed)
            {
                transform.localPosition = Bezier.GetPoint(a, b, c, t);
                Vector3 d = Bezier.GetDerivative(a, b, c, t);
                d.y = 0f;
                transform.localRotation = Quaternion.LookRotation(d);
                yield return null;
            }
            transform.localPosition = location.Position;
            orientation = transform.localRotation.eulerAngles.y;

            ListPool<HexCell>.Add(pathToTravel);
            pathToTravel = null;
        }

        public int GetMoveCost(HexCell fromCell, HexCell toCell, HexDirection direction)
        {
            HexEdgeType edgeType = fromCell.GetEdgeType(toCell);
            if (edgeType == HexEdgeType.Cliff)
            {
                return -1;
            }
            int moveCost;
            if (fromCell.HasRoadThroughEdge(direction))
            {
                moveCost = 1;
            }
            else if (fromCell.Walled != toCell.Walled)
            {
                return -1;
            }
            else
            {
                moveCost = edgeType == HexEdgeType.Flat ? 5 : 10;
            }

            return moveCost;
        }

        private void OnEnable()
        {
            if (location)
            {
                transform.localPosition = location.Position;

                if (currentTravelLocation)
                {
                    Grid.IncreaseVisibility(location, VisionRange);
                    Grid.DecreaseVisibility(currentTravelLocation, VisionRange);
                    currentTravelLocation = null;
                }
            }
        }

        public void Die()
        {
            if (location)
            {
                Grid.DecreaseVisibility(location, VisionRange);
            }
            location.Unit = null;
            Destroy(gameObject);
        }

        private IEnumerator LookAt(Vector3 point)
        {
            point.y = transform.localPosition.y;

            Quaternion fromRotation = transform.localRotation;
            Quaternion toRotation = Quaternion.LookRotation(point - transform.localPosition);
            float angle = Quaternion.Angle(fromRotation, toRotation);
            if (angle > 0f)
            {
                float speed = rotationSpeed / angle;

                for (float t = Time.deltaTime * speed; t < 1f; t += Time.deltaTime * speed)
                {
                    transform.localRotation = Quaternion.Slerp(fromRotation, toRotation, t);
                    yield return null;
                }
            }

            transform.LookAt(point);
            orientation = transform.localRotation.eulerAngles.y;
        }

        public void Save(BinaryWriter writer)
        {
            location.coordinates.Save(writer);
            writer.Write(orientation);
        }

        public static void Load(BinaryReader reader, HexGrid grid)
        {
            HexCoordinates coordinates = HexCoordinates.Load(reader);
            float orientation = reader.ReadSingle();
            grid.AddUnit(Instantiate(unitPrefab), grid.GetCell(coordinates), orientation);
        }
    }
}