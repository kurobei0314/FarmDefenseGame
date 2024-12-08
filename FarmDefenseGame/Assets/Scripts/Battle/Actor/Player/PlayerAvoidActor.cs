using WolfVillageBattle.Interface;
using WolfVillage.Entity.Interface;

public class PlayerAvoidActor : IPlayerAvoidUseCase
{
    private IPlayerView _playerView;
    private IPlayerEntity _playerEntity;
    private ICameraView _cameraView;

    public PlayerAvoidActor(IPlayerView playerView, IPlayerEntity playerEntity, ICameraView cameraView)
    {
        _playerView = playerView;
        _playerEntity = playerEntity;
        _cameraView = cameraView;
    }
    
    public void AvoidEnemy(float horizontalInput, float verticalInput)
    {
        if (_playerEntity.CurrentStatus != Status.Idle) return;
        _playerEntity.SetStatus(Status.Avoid);
        var moveDirection = (horizontalInput != 0 && verticalInput != 0) ?
            (_cameraView.CameraTrans.forward * verticalInput + _cameraView.CameraTrans.right * horizontalInput).normalized : 
            _playerView.unityChan.transform.forward;
        _playerView.Avoid(moveDirection);
    }
}
