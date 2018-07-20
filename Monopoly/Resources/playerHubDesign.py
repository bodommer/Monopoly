import tkinter
import PIL
from PIL import Image, ImageDraw, ImageFont, ImageOps

image = Image.new('RGBA', (424,179), 'white')
canvas = ImageDraw.Draw(image)

opacity1 = 100
opacity2 = 100
color1 = (218, 165, 32)
color2 = (255, 215, 0)

canvas.rectangle((0,0,424, 89), fill=color1, outline=color1)
canvas.rectangle((0, 90, 424, 179), fill=color2, outline=color2)

image.save("playerHub.png")

#colors = [(255, 0, 0, 0), (0, 255, 0, 0), (0, 0, 255, 0), (255, 255, 0, 0),
#          (255, 0, 255, 0), (0, 255, 255, 0)]
colors = ('red', 'blue', 'green', 'yellow', 'brown', 'purple')

for i in range(6):
    image = Image.new('RGBA', (20, 20), (0,0,0,0))
    canvas = ImageDraw.Draw(image)

    canvas.ellipse((0,0, 19, 19), fill=colors[i], outline=colors[i])

    image.save("player{}.png".format(i+1))

    
