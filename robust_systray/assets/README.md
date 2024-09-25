# Generating .ICO files

## Prep

```pwsh
winget install ImageMagick.ImageMagick
```

## Inner loop

1. Edit the icon
2. Export as SVG
3. Convert to ICO files using the `magick` command below:

```pwsh
magick simple_icon.svg -alpha off -resize 32x32 `
          -define icon:auto-resize="32,16" `
          simple_icon.ico
```