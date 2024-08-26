using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Block/Setting")]
public class BlockSetting : ScriptableObject
{
    public Sprite spriteButton;
    public Color color = Color.white;
    public Material material;
    public int damage = 5;
    public LayerMask layer;
    public GameObject Object;
}
