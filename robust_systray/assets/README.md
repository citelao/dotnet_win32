# Generating .ICO files

## Prep

```pwsh
winget install ImageMagick.ImageMagick
```

## Inner loop

1. Edit the icon
2. Export as PNG
3. Convert to BMP files using the `magick` command below:

```
magick simple_icon.png `
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
magick simple_icon.svg `
        -alpha on `
        -resize 32x32 `
        -transparent white `
        -define icon:auto-resize="32,16" `
        -background none `
        simple_icon.ico
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