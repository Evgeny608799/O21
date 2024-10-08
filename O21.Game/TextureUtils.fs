// SPDX-FileCopyrightText: 2024 O21 contributors <https://github.com/ForNeVeR/O21>
//
// SPDX-License-Identifier: MIT

module O21.Game.TextureUtils

open JetBrains.Lifetimes
open Microsoft.FSharp.NativeInterop
open Raylib_CsLo
open type Raylib_CsLo.Raylib
open Oddities.Resources

let private transparentColor = struct(0xFFuy, 0xFFuy, 0xFFuy)
let private isColor(struct(r1, g1, b1), struct(r2, g2, b2)) =
    r1 = r2 && g1 = g2 && b1 = b2

#nowarn "9"

let CreateTransparentSprite (lifetime: Lifetime) (colors: Dib) (transparency: Dib): Texture =
    let width = colors.Width
    let height = colors.Height
    let colors = Array.init (width * height) (fun i ->
        let x = i % width
        let y = i / width
        let isTransparent = isColor(transparency.GetPixel(x, y), transparentColor)
        if isTransparent then
            BLANK
        else
            let struct(r, g, b) = colors.GetPixel(x, y)
            Color(r, g, b, 255uy)
    )
    use colorsPtr = fixed colors
    let image = Image(
        data = NativePtr.toVoidPtr colorsPtr,
        width = width,
        height = height,
        format = int PixelFormat.PIXELFORMAT_UNCOMPRESSED_R8G8B8A8,
        mipmaps = 1
    )
    RaylibUtils.LoadTextureFromImage lifetime image

let CreateSprite (lifetime: Lifetime) (colors: Dib): Texture =
    let width = colors.Width
    let height = colors.Height
    let colors = Array.init (width * height) (fun i ->
        let x = i % width
        let y = i / width
        let struct(r, g, b) = colors.GetPixel(x, y)
        Color(r, g, b, 255uy)
    )
    use colorsPtr = fixed colors
    let image = Image(
        data = NativePtr.toVoidPtr colorsPtr,
        width = width,
        height = height,
        format = int PixelFormat.PIXELFORMAT_UNCOMPRESSED_R8G8B8A8,
        mipmaps = 1
    )
    RaylibUtils.LoadTextureFromImage lifetime image
