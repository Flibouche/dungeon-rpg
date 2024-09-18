using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] private AnimationPlayer animPlayerNode;
    [Export] private Sprite3D spriteNode;

    private Vector2 direction = new();

    // Called when the node enters the scene tree for the first time
    public override void _Ready()
    {
        animPlayerNode.Play(GameConstants.ANIM_IDLE);
    }

    // Called every frame
    public override void _PhysicsProcess(double delta)
    {
        Velocity = new(direction.X, 0, direction.Y);
        Velocity *= 5;

        MoveAndSlide();

        Flip();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT, // Negative X
            GameConstants.INPUT_MOVE_RIGHT, // Positive X
            GameConstants.INPUT_MOVE_FORWARD, // Negative Y
            GameConstants.INPUT_MOVE_BACKWARD // Positive Y
        );

        if (direction == Vector2.Zero)
        {
            animPlayerNode.Play(GameConstants.ANIM_IDLE);
        }
        else
        {
            animPlayerNode.Play(GameConstants.ANIM_MOVE);
        }
    }

    private void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally) { return; }

        bool isMovingLeft = Velocity.X < 0;
        spriteNode.FlipH = isMovingLeft;
    }
}
