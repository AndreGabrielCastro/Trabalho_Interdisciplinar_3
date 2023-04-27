using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerEvent : MonoBehaviour
{
    public static CameraControllerEvent Instance;

    [Header("- - - Setted during playtime - - -")]
    private Vector3 cameraOriginalLocalPosition;
    private Vector3 shakeDirection;
    private sbyte shakeOriginalIntensity;
    private float shakeCurrentIntensity;
    private float shakeOriginalDuration;
    private float shakeCurrentDuration;
    private bool isShakingEnding;
    private bool isShaking;

    private void Awake()
    {
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogWarning($" ----- There is more than one Camera Controller Event!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }
    public void ShakeCamera(Vector3 direction, byte intensity = 4, float duration = 0.3f)
    {
        if (isShaking == false) { cameraOriginalLocalPosition = Player.Instance.playerCameraFakeTransform.localPosition; }
        shakeDirection = direction;
        shakeOriginalIntensity = (sbyte)(intensity * 25f);
        shakeCurrentIntensity = intensity * 25;
        shakeOriginalDuration = duration;
        shakeCurrentDuration = 0;
        isShakingEnding = false; isShaking = true;
    }

    private void FixedUpdate()
    {
        if (isShaking == true)
        {
            // SHAKE CALCULATION
            if (isShakingEnding == false)
            {
                // OFFSET CALCULATION
                Vector3 offset = Vector3.zero;
                offset = new Vector3(Random.Range(0.02f, -0.02f),
                                     Random.Range(0.02f, -0.02f),
                                     Random.Range(0.02f, -0.02f)) * shakeCurrentIntensity;

                // EXTRA OFFSET
                if (shakeDirection != Vector3.zero)
                {
                    offset += new Vector3(Random.Range(0.02f, 0.01f) * shakeDirection.x,
                                          Random.Range(0.02f, 0.01f) * shakeDirection.y,
                                          Random.Range(0.02f, 0.01f) * shakeDirection.z) * shakeCurrentIntensity;
                }

                // CAMERA POSITION ALTERATION
                Player.Instance.playerCameraFakeTransform.localPosition = Vector3.Lerp(Player.Instance.playerCameraFakeTransform.localPosition,
                                                                                       Player.Instance.playerCameraFakeTransform.localPosition + offset,
                                                                                       Time.fixedDeltaTime);

                // CAMERA ROTATION CALCULATION
                Vector3 lookDirection = (new Vector3(Player.Instance.playerCameraFakeTransform.localPosition.x * 2,
                                                     0,
                                                     Player.Instance.playerCameraFakeTransform.localPosition.z * 2)
                                        - Player.Instance.playerCameraFakeTransform.localPosition).normalized;

                // CAMERA ROTATION ALTERATION
                Player.Instance.playerCameraFakeTransform.up = -lookDirection;


                // INTENSITY ALTERATION
                shakeCurrentIntensity -= (Time.fixedDeltaTime * shakeOriginalIntensity * 2) / shakeOriginalDuration;
                if (shakeCurrentIntensity <= -shakeOriginalDuration) { isShakingEnding = true; }
            }
            else
            {
                // CAMERA POSITION RETURN
                Player.Instance.playerCameraFakeTransform.localPosition = Vector3.Lerp(Player.Instance.playerCameraFakeTransform.localPosition,
                                                                                       cameraOriginalLocalPosition,
                                                                                       Time.fixedDeltaTime * 2 + 0.02f);

                // CAMERA ROTATION RETURN
                Player.Instance.playerCameraFakeTransform.up = Vector3.Lerp(Player.Instance.playerCameraFakeTransform.up,
                                                                            Vector3.up,
                                                                            Time.fixedDeltaTime * 2 + 0.02f);

                // FINAL ALTERATION
                if ((Player.Instance.playerCameraFakeTransform.localPosition - cameraOriginalLocalPosition).magnitude <= 0.001f)
                {
                    Player.Instance.playerCameraFakeTransform.localPosition = cameraOriginalLocalPosition;
                    Player.Instance.playerCameraFakeTransform.rotation = Quaternion.identity;
                    isShaking = false;
                    return;
                }
            }
        }
    }
}