using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Vector3 screenOffset = new Vector3(0f, 50f, 0f);

    float characterControllerHeith = 1f;
    Transform targetTransform;
    Renderer targetRenderer;
    CanvasGroup _canvasGroup;
    Vector3 targetPosition;

    private AnimatorController target;

    private void Awake()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
        this.transform.SetParent(GameObject.Find("Canvas/PlayerUI_Group").GetComponent<Transform>(), false);
    }

    private void Start()
    {
        playerHealthSlider.value = playerHealthSlider.maxValue;
    }

    private void Update()
    {     
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        playerHealthSlider.value = target.health;

    }

    private void LateUpdate()
    {
        if (targetRenderer != null)
        {
            this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
        }

        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeith;
            this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
        }
    }

    public void SetTarget(AnimatorController _target)
    {
      
        target = _target;
        playerNameText.text = target.photonView.Owner.NickName;
        targetTransform = this.target.GetComponent<Transform>();
        targetRenderer = this.target.GetComponent<Renderer>();
        CharacterController characterController = _target.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterControllerHeith = characterController.height;
        }
    }
}
