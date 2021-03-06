using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    WebCamTexture _webCamTexture;

    CascadeClassifier cascade;

    OpenCvSharp.Rect MyFace;

    public float faceY;

    public float faceX;

    private OpenCvSharp.Rect previousFaces;

    void Start()
    {
        // initialisiert die Webcam
        WebCamDevice[] devices = WebCamTexture.devices;

        _webCamTexture = new WebCamTexture(devices[0].name, 1280, 720);
        _webCamTexture.Play();
        cascade =
            new CascadeClassifier(Application.dataPath +
                @"/haarcascade_frontalface_default.xml");
    }

    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = _webCamTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

        findNewFace (frame);

        display (frame);
    }

    void findNewFace(Mat frame)
    {
        // findet ein Gesicht im Webcamstream und setzt die x und y Koordinaten
        OpenCvSharp.Rect[] faces =
            cascade
                .DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

        if (previousFaces != null && previousFaces.Height * 0.6f >= faces[0].Height) return;

        float widthCam = _webCamTexture.width;
        float heightCam = _webCamTexture.height;

        if (faces.Length >= 1)
        {
            MyFace = faces[0];
            faceY = faces[0].Location.Y / heightCam;
            faceX = faces[0].Location.X / widthCam;
        }

        previousFaces = faces[0];
    }

    void display(Mat frame)
    {
        // stellt den Webcamstream in der Anwendung dar
        if (MyFace != null)
        {
            frame.Rectangle(MyFace, new Scalar(250, 0, 0), 2);
        }

        Texture newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newtexture;
    }
}
