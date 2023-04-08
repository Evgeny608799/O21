namespace O21.Game

open Microsoft.Xna.Framework.Graphics

open O21.Game.U95

type IGameScene =
    abstract member Update: GameWorld -> Input -> Time -> GameWorld
    abstract member Render: SpriteBatch -> U95Data -> GameWorld -> unit
and GameWorld = {
    Scene: IGameScene
    CurrentLevel: Level
}