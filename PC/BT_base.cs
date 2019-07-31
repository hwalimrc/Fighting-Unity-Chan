using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class TreeNode
{
    public abstract bool Invoke();
    // AI 캐릭터의 행동을 나타낼 함수를 추상 메소드로 선언하였다.
    // AI 캐릭터의 행동에 따라 재정의 하여 나타낸다.
    
}

public class CompositeNode : TreeNode
{
    private Stack<TreeNode> childrens = new Stack<TreeNode>();

    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(TreeNode newNode)
    {
        childrens.Push(newNode);
        // 새로운 하위 노드가 추가되었을 때.
    }

    public Stack<TreeNode> GetChildrens()
    {
        return childrens;
    }
}

public class Selector : CompositeNode
{
    public override bool Invoke()
    {
        foreach (var childNode in GetChildrens())
        {
            if (childNode.Invoke()) return true;
        }
        return false;
    }
}

public class Sequence : CompositeNode
{
    public override bool Invoke()
    {
        foreach (var childNode in GetChildrens())
        {
            if (!childNode.Invoke()) return false; 
        }
        return true;
    }
}

public class MoveForTarget : TreeNode
{
    public Enemy enemyController
    {
        set { _monController = value; }
    }
    private Enemy _monController;
    public override bool Invoke()
    {
        //_monController.MoveForTarget();
        return true;
    }
}