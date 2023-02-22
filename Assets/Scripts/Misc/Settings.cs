using UnityEngine;

public static class Settings
{
    #region Player Settings

    public const int maxHealth = 5;
    public const float playerSpeed = 5f;
    public const float attackRate = 0.7f;
    public const float projectileForce = 300f;
    public const float timeInvincible = 2f;

    #endregion

    #region  Player Animations Parameters

    public static int lookX;
    public static int lookY;
    public static int animSpeed;
    public static int launch;
    public static int hit;
    
    #endregion

    #region Enemy Settings

    public const int enemyMaxHealth = 3;
    public const int enemyDamage  = -1;
    public const float enemySpeed = 3f;

    #endregion

    #region Enemy Animations Parameters

    public static int moveX;
    public static int moveY;

    #endregion

    #region Drop Items

    public const int coin = 0;
    public const int gem = 1;
    public const int heart = 2;
    
    #endregion

    #region Game Sounds

    public const int arrowSound = 0;
    public const int heartSound = 1;
    public const int collectableSound = 2;
    public const int hurtSound = 3;
    public const int questSound = 4;
    public const int mobDeathSound = 5;

    #endregion

    static Settings()
    {
        // Player

        lookX = Animator.StringToHash("Look X");
        lookY = Animator.StringToHash("Look Y");
        animSpeed = Animator.StringToHash("Speed");
        launch = Animator.StringToHash("Launch");
        hit = Animator.StringToHash("Hit");

        // Enemy

        moveX = Animator.StringToHash("Move X");
        moveY = Animator.StringToHash("Move Y");
    }

}