using Meta.WitAi;
using Meta.WitAi.Requests;
using UnityEngine;
using UnityEngine.UI;

namespace Oculus.VoiceSDK.UX
{
    public class TriggerVoiceDetection : MonoBehaviour
    {
        // The button to be observed
        [SerializeField] Button _button;
        // The button label to be adjusted with state
        [SerializeField] Text _buttonLabel;

        [Tooltip("Reference to the current voice service")]
        [SerializeField] private VoiceService _voiceService;

        [Tooltip("Text to be shown while the voice service is not active")]
        [SerializeField] private string _activateText = "Activate";
        [Tooltip("Whether to immediately send data to service or to wait for the audio threshold")]
        [SerializeField] private bool _activateImmediately = false;

        [Tooltip("Text to be shown while the voice service is active")]
        [SerializeField] private string _deactivateText = "Deactivate";
        [Tooltip("Whether to immediately abort request activation on deactivate")]
        [SerializeField] private bool _deactivateAndAbort = false;

        // Current request
        private VoiceServiceRequest _request;
        private bool _isActive = false;

        // Get button & label
        private void Awake()
        {
           // _buttonLabel = GetComponentInChildren<Text>();
            //_button = GetComponent<Button>();
            if (_voiceService == null)
            {
                _voiceService = FindObjectOfType<VoiceService>();
            }
        }
        // Add click delegate
        private void OnEnable()
        {
            RefreshActive();
            if (_button != null)
            {
                _button.onClick.AddListener(OnClick);
            }
        }
        // Remove click delegate
        private void OnDisable()
        {
            _isActive = false;
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClick);
            }
        }

        // On click, activate if not active & deactivate if active
        private void OnClick()
        {
            if (!_isActive)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }

        // Activate depending on settings
        private void Activate()
        {
            if (!_activateImmediately)
            {
                _request = _voiceService.Activate(GetRequestEvents());
            }
            else
            {
                _request = _voiceService.ActivateImmediately(GetRequestEvents());
            }
        }

        // Deactivate depending on settings
        private void Deactivate()
        {
            if (!_deactivateAndAbort)
            {
                _request.DeactivateAudio();
            }
            else
            {
                _request.Cancel();
            }
        }

        // Get events
        private VoiceServiceRequestEvents GetRequestEvents()
        {
            VoiceServiceRequestEvents events = new VoiceServiceRequestEvents();
            events.OnInit.AddListener(OnInit);
            events.OnComplete.AddListener(OnComplete);
            return events;
        }
        // Request initialized
        private void OnInit(VoiceServiceRequest request)
        {
            _isActive = true;
            RefreshActive();
        }
        // Request completed
        private void OnComplete(VoiceServiceRequest request)
        {
            _isActive = false;
            RefreshActive();
        }

        // Refresh active text
        private void RefreshActive()
        {
            if (_buttonLabel != null)
            {
                _buttonLabel.text = _isActive ? _deactivateText : _activateText;
            }
        }
        public void ActivateVoice()
        {
            if (!_activateImmediately)
            {
                _request = _voiceService.Activate(GetRequestEvents());
            }
            else
            {
                _request = _voiceService.ActivateImmediately(GetRequestEvents());
            }
        }
    }

    
}
