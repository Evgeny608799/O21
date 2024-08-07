module O21.Game.Engine.GameRules

open System

[<Literal>]
let ShotCooldownTicks = 15

[<Literal>]
let LevelWidth = 600

[<Literal>]
let LevelHeight = 400

[<Literal>]
let TicksPerSecond = 10.0

let MaxPlayerVelocity = 3
let ClampVelocity(Vector(x, y)): Vector =
   Vector(
       Math.Clamp(x, -MaxPlayerVelocity, MaxPlayerVelocity),
       Math.Clamp(y, -MaxPlayerVelocity, MaxPlayerVelocity)
   )

[<Literal>]
let BulletVelocity = 5 // TODO[#131]: Compare with the original

[<Literal>]
let ParticleVelocity = 3

let ParticlesPeriodRange = [|7..10|]
let ParticlesOffsetRange = [|-2..2|]

let PlayerParticlesDirection = VerticalDirection.Up

let PlayerSize = Vector(46, 27)
let BulletSize = Vector(6, 6)
let ParticleSize = Vector(5, 5)

/// Relative position of the bullet sprite's top left corner to the player sprite's top corner (from the shooting side)
/// when the bullet appears.
let NewBulletPosition(playerTopForwardCorner: Point, playerDirection: HorizontalDirection) =
    match playerDirection with
    | HorizontalDirection.Left -> playerTopForwardCorner + Vector(-4 - BulletSize.X, 14)
    | HorizontalDirection.Right -> playerTopForwardCorner + Vector(4, 14)

let NewParticlePosition(playerTopForwardCorner: Point, playerDirection: HorizontalDirection) =
    match playerDirection with
    | HorizontalDirection.Left -> playerTopForwardCorner + Vector(28, -ParticleSize.Y)
    | HorizontalDirection.Right -> playerTopForwardCorner + Vector(-28, -ParticleSize.Y)
