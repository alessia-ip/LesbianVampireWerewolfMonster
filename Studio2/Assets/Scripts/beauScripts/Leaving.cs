using UnityEngine;

namespace beauScripts
{
  public class Leaving : MonoBehaviour
  {
    public OpenMap map;
    public GameObject mapCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag.Contains("Door"))
      {
        map.OpenMapUI();
      }
    }
  }
}
