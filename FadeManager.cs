using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// �t�F�[�h�C���A�E�g�̊Ǘ�
/// </summary>
public class FadeManager : MonoBehaviour
{
	/// <summary>
	/// �t�F�[�h�C���A�E�g��������I�u�W�F�N�g
	/// </summary>
	[SerializeField] private Image _fadeImage;

	/// <summary>
	/// �t�F�[�h�A�E�g�X�e�[�^�X
	/// </summary>
	private enum FADE_MODE
	{
		NONE,
		FADE_IN,
		FADE_OUT,
	}

	private FADE_MODE _fadeMode = FADE_MODE.NONE;

	/// <summary>
	/// �O������A�N�Z�X���邽��static�ɂ���
	/// </summary>
	private static FadeManager _main;

	public static FadeManager GetInstance()
	{
		return _main;
	}

	/// <summary>
	/// Start��葁�����s�����
	/// </summary>
	private void Awake()
	{
		//�F��������
		_fadeImage.color = new Color(0, 0, 0, 0);
		//�I�u�W�F�N�g������
		_fadeImage.gameObject.SetActive(false);
		_main = this;
	}

	private void Update()
	{
		switch (_fadeMode)
		{
			//���񂾂�Â��Ȃ�
			case FADE_MODE.FADE_OUT:
				{
					var color = _fadeImage.color;
					//�����x��Z�����Ă���
					color.a += Time.deltaTime;
					_fadeImage.color = color;
					if (color.a >= 1.0f)
					{
						color.a = 1.0f;
						_fadeMode = FADE_MODE.NONE;
					}
					break;
				}
			//���񂾂񖾂邭�Ȃ�
			case FADE_MODE.FADE_IN:
				{
					var color = _fadeImage.color;
					//�����x���グ�Ă���
					color.a -= Time.deltaTime;
					_fadeImage.color = color;
					if (color.a <= 0.0f)
					{
						color.a = 0.0f;
						_fadeMode = FADE_MODE.NONE;
						_fadeImage.gameObject.SetActive(false);
					}
					break;
				}
		}

	}

	/// <summary>
	/// �t�F�[�h�C���J�n
	/// ���񂾂�Â��Ȃ�
	/// </summary>
	public void StartFadeIn()
	{
		_fadeMode = FADE_MODE.FADE_IN;
		_fadeImage.color = new Color(0, 0, 0, 1);
		_fadeImage.gameObject.SetActive(true);
	}

	/// <summary>
	/// �t�F�[�h�A�E�g�J�n
	/// ���񂾂񖾂邭
	/// </summary>
	public void StartFadeOut()
	{
		_fadeMode = FADE_MODE.FADE_OUT;
		_fadeImage.color = new Color(0, 0, 0, 0);
		_fadeImage.gameObject.SetActive(true);
	}
}
