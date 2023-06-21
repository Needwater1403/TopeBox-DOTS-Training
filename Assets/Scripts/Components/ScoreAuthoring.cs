using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ScoreAuthor : MonoBehaviour
{
    public int score = 0;
    public class ScoreBaker : Baker<ScoreAuthor>
    {
        public override void Bake(ScoreAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new ScoreComponent
            {
                score = authoring.score,
            });
        }
    }
}
