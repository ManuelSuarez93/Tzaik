namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerInteract
    {
        public void Interact(IInteractable interactable)
            => interactable?.DoInteraction();
    }

    public interface IInteractable
    {
        public void DoInteraction();
    }

}