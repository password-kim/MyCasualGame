using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteColorTween : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    private Color originColor; // 원래 갖고 있던 색깔.
    private Color targetColor; // 변하고 싶은 색깔.

    public enum TweenType
    {
        None = -1,
        Foward = 0,
        Backward = 1,
    }

    public TweenType state = TweenType.None;
    private float speed = 0.0f;
    private float elapsedTime = 0.0f;

    public void SetTween(Color targetColor, float changeSpeed)
    {
        // 현재 트윈중이면 리턴.
        if (state != TweenType.None)
            return;

        originColor = myRenderer.color;
        this.targetColor = targetColor;
        speed = changeSpeed;
        state = TweenType.Foward;
        elapsedTime = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TweenType.Foward:
                elapsedTime += Time.deltaTime * speed;
                myRenderer.color = Color.Lerp(originColor, targetColor, elapsedTime);
                if (myRenderer.color.Equals(targetColor))
                {
                    elapsedTime = 0.0f;
                    state = TweenType.Backward;
                }
                break;
            case TweenType.Backward:
                elapsedTime += Time.deltaTime * speed;
                myRenderer.color = Color.Lerp(targetColor, originColor, elapsedTime);
                if (myRenderer.color.Equals(originColor))
                {
                    elapsedTime = 0.0f;
                    state = TweenType.None;
                }
                break;
        }
    }
}
