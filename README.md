# EMOTION RECOGNITION DEMO

A Winform app detect user's emotion from PC webcam

## Getting Started

So far, this is one of my Toy projects in 2018. Through this project, the author want to learn Neural Network and reinforce my C# coding skill.
Thank for your attention and wish you have as much good time as I had with this demo.

## Prerequisites

* [EmguCV v3.4.1.2976](https://sourceforge.net/projects/emgucv/?source=typ_redirect) - OpenCV C# Wrapper

Or

Install EmguCV as a NuGet package by command

```
PM> Install-Package Emgu.CV -Version 3.4.1.2976
```

* Anaconda, Python
*
## Tutorial

First, we will create a winform to capture data frames from webcam device by ~~**avicap32 dll lib**~~ EmguCV.

## Contributing

## Versioning

## License

## Acknowledgements
### Articles
* [Paulvangent's Emotion Recognition Using Facial Landmark](http://www.paulvangent.com/2016/08/05/emotion-recognition-using-facial-landmarks/)

* [Algorithmia's Emotion Recognition](https://blog.algorithmia.com/introduction-to-emotion-recognition/)

* [Satya Mallick's Facial Landmark Detection](https://www.learnopencv.com/facial-landmark-detection/)

### Publications

* [HAL's Introduction to Emotion Recognition for Digital Images](https://hal.inria.fr/inria-00561918/PDF/Tutorial-Introduction_to_Emotion_Recognition_for_Digital_Images.pdf)

* [USC's Analysis of Emotion Recognition Using Facial Expressions and others information](http://sail.usc.edu/publications/files/Busso_2004.pdf)

## Logs

> This is my personal logs about the obstacles I met while doing this project.

Apr 8, 2018
```
Began to design a draft for Winform App. Quite easy *LOL*.

Then, stuck right at the webcam data retrieving process. After a brief google search I found a solution using avicap.dll.

OK, webcam's video displayed successfully. But, I concidently found that using *avicap.dll* was deprecated.

There are currently (in 2018) 3 ways to get data from your webcam:

- WIA
- Avicap
- DirectShow (or DirectShowNet - a wrapper for C#)

Yeah, somehow I realized that my 30 mins of work was for nothing.

Note: Alway research carefully before writing any of code.
```

Apr 9, 2018
```

```