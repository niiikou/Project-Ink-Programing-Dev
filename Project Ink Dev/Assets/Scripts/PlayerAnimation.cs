using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
  //Rigidbody m_Rigidbody;
  Animator m_Animator;

  [Header("转向速度")]
  [SerializeField]
  private float m_turnSpeed = 20;
  // 四元数移动模型朝向
  Quaternion m_Rotation = Quaternion.identity;
  // Start is called before the first frame update
  void Start()
  {
    // 获取动画组件
    m_Animator = GetComponent<Animator>();
    // 获取刚体组件
    //m_Rigidbody = GetComponent<Rigidbody>();
  }
  //private void OnAnimatorMove()
  //{
  //  m_Rigidbody.MoveRotation(m_Rotation);
  //}
  private void TargetPlayerRotation(Vector3 movement)
  {
    // 计算往目标朝向过度的矢量
    Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, m_turnSpeed * Time.deltaTime, 0f);
    m_Rotation = Quaternion.LookRotation(desiredForward);
  }
  /// <summary>
  /// 动画进入移动或待机状态<para />
  /// <param name="movement">模型最终朝向向量<para /></param>
  /// <param name="isExitIdle">是否离开待机状态，true为移动，false为回到待机(idle)<para /></param>
  /// <param name="status">walk or jump or run(run is not exist)<para /></param>
  /// <returns>无返回</returns>
  /// </summary>
  public void PlayerStatusChange(Vector3 movement, bool isExitIdle, string status)
  {
    // 单位化方向向量
    movement.Normalize();
    // 播放status动画
    m_Animator.SetBool(status, isExitIdle);
  }
  /// <summary>
  /// 修改模型转向速度<para />
  /// <param name="newSpeed">模型最终朝向向量<para /></param>
  /// <returns>无返回</returns>
  /// </summary>
  public void TurnSpeedChange(float newSpeed)
  {
    m_turnSpeed = newSpeed;
  }
}
