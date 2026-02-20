using UnityEngine;

public class WaveSpawnManagerExam04 : MonoBehaviour
{
    public Wave[] waveConfigurations;
    public WaveController waveController;

    public bool enableWaveCycling;

    private int currentWave = 0;
    private float waveEndTime = 0f;

    void Start()
    {
        if (waveConfigurations == null || waveConfigurations.Length == 0 || waveController == null)
        {
            Debug.LogError("WaveSpawnManagerExam04: Missing waveConfigurations or waveController");
            enabled = false;
            return;
        }

        currentWave = 0;
        waveController.StartWave(waveConfigurations[currentWave]);
        waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval; // ✅ set ให้ wave แรกด้วย
    }

    void Update()
    {
        if (currentWave >= waveConfigurations.Length)
        {
            return;
        }
        //if wave time is up and all enemies are spawned, start next wave
        if (Time.time >= waveEndTime && waveController.IsComplete())
        {
            currentWave++;
            if (currentWave >= waveConfigurations.Length)
            {
                if (enableWaveCycling) // ถ้าเปิดใช้งานการวนลูป ให้กลับไปที่ wave แรก
                {
                    
                    currentWave = 0;
                }
                else
                {
                    Debug.Log("All waves completed!");
                    return;
                }
            }

            // เริ่ม wave ใหม่
            waveController.StartWave(waveConfigurations[currentWave]);
            waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
        }
    }
}