using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShotAttack : AICharacterAttack
{
    Enemy enemy;

    public override void Set(Enemy character)
    {
        enemy = character;



    }

    public override void Attack(Vector3 dir, Vector3 StartPos, Character characterOponent = null, Character character = null)
    {

        dir = (characterOponent.transform.position - character.transform.position);

        dir = new Vector3(dir.x, 0, dir.z).normalized;

        character.transform.LookAt(characterOponent.transform);

        var go = GameScene.Instance.ObjectPoolManager.UseObject(GameObjectType.Projectile, enemy.characterInfo.Shot_Id, character.transform.position);

        if (go == null)
        {
            return;
        }

        Projectile projectile = go.GetComponent<Projectile>();

        projectile.Fire(enemy.characterInfo, dir);

        enemy.AIRefresh();

    }



}
