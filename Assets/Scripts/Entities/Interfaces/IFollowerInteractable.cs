using Entities.Follower;
using Entities.Player;

interface IFollowerInteractable : IInteractable {
    /// <summary>
    /// Executes code when interacted and outputs the interacted follower.
    /// </summary>
    /// <param name="interactor">The player that interacted.</param>
    /// <param name="follower">The interacted follower</param>
    void FollowerInteraction(PlayerController interactor, out FollowerController follower);
}
