public class Medicine : Item
{
    private void OnEnable()
    {
        Taken += OnTaken;
    }

    private void OnDisable()
    {
        Taken -= OnTaken;
    }

    private void OnTaken()
    {
        Destroy(gameObject);
    }
}
