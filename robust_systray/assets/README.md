# Generating .ICO files

## Prep

```pwsh
winget install ImageMagick.ImageMagick
```

## Inner loop

1. Edit the icon
2. Export as SVG
3. Convert to ICO files using the `magick` command below:

```
magick `
        -background none `
        simple_icon.svg `
        -resize 32x32 `
        -define icon:auto-resize="32,16" `
        simple_icon.ico
```

---

See also:

```
magick simple_icon.png `
        simple_icon.bmp
```

```pwsh
magick `
        -background none `
        simple_icon.svg `
        -resize 64x64 `
        simple_icon.png
```

```
magick `
        -background none `
        simple_icon.svg `
        -resize 32x32 `
        -define icon:auto-resize="32,16" `
        simple_icon.ico
```

Adapted from https://www.imagemagick.org/Usage/thumbnails/#favicon