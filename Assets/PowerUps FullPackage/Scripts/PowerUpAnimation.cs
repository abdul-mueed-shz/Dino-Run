namespace VisCircle {

    using UnityEngine;
    using System.Collections;

    public class PowerUpAnimation : MonoBehaviour {
        [SerializeField]
        private bool _animateRotation = true;
        public Vector3 rotationSpeedsInDegreePerSecond;
        public RotationType rotationType = RotationType.SelfAxis;

        [SerializeField]
        private bool _animateScale = true;
        public float scaleMin = 0.5f, scaleMax = 1.5f, scaleCycleDuration = 5;

        [SerializeField]
        private bool _animateYOffset = true;
        public float yOffsetAmplitude = 1, yOffsetCycleDuration = 5;

        private Vector3 _startLocalPosition;
        private Quaternion _startLocalRotation;
        private Vector3 _startLocalScale;

        private Transform _transform;

        void Awake() {
            _transform = this.GetComponent<Transform>();

            _startLocalPosition = _transform.localPosition;
            _startLocalRotation = _transform.localRotation;
            _startLocalScale = _transform.localScale;
        }

        void Update() {
            if (_animateYOffset) {
                float yOff;
                if (yOffsetCycleDuration != 0) {
                    yOff = Mathf.Sin(Time.time / yOffsetCycleDuration * Mathf.PI * 2) * yOffsetAmplitude;
                } else {
                    yOff = 0;
                }

                this.transform.localPosition = _startLocalPosition + new Vector3(0, yOff, 0);
            }

            if (_animateScale) {
                float scale;
                if (scaleCycleDuration != 0) {
                    float scaleT = Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time / scaleCycleDuration * Mathf.PI * 2));
                    scale = Mathf.Lerp(scaleMin, scaleMax, scaleT);
                } else {
                    scale = 1;
                }

                this.transform.localScale = scale * _startLocalScale;
            }

            if (_animateRotation) {
                if (rotationType == RotationType.WorldAxis) {

                    this.transform.Rotate(rotationSpeedsInDegreePerSecond * Time.deltaTime, Space.World);
                } else {
                    this.transform.Rotate(rotationSpeedsInDegreePerSecond * Time.deltaTime, Space.Self);
                }
            }
        }

        public bool GetAnimateScale() { return _animateScale; }
        public void SetAnimateScale(bool newAnimateScale) {
            this._animateScale = newAnimateScale;

            if (!_animateScale && Application.isPlaying) {
                this.transform.localScale = _startLocalScale;
            }
        }

        public bool GetAnimateYOffset() { return _animateYOffset; }
        public void SetAnimateYOffset(bool newAnimateYOffset) {
            this._animateYOffset = newAnimateYOffset;

            if (!_animateYOffset && Application.isPlaying) {
                this.transform.localPosition = _startLocalPosition;
            }
        }


        public bool GetAnimateRotation() { return _animateRotation; }
        public void SetAnimateRotation(bool newAnimateRotation) {
            this._animateRotation = newAnimateRotation;

            if (!_animateRotation && Application.isPlaying) {
                this.transform.localRotation = _startLocalRotation;
            }
        }

        public enum RotationType { SelfAxis, WorldAxis }
    }
}
