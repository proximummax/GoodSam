using UnityEngine;
using UnityEngine.AI;

namespace TheKiwiCoder {


    public class Context {
        public GameObject gameObject;
        public Transform transform;
        public AIAnimationController animationComponent;
        public Rigidbody physics;
        public NavMeshAgent agent;
        public SphereCollider sphereCollider;
        public BoxCollider boxCollider;
        public CapsuleCollider capsuleCollider;
        public CharacterController characterController;

        public static Context CreateFromGameObject(GameObject gameObject) {

            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animationComponent = gameObject.GetComponent<AIAnimationController>();
            context.physics = gameObject.GetComponent<Rigidbody>();
            context.agent = gameObject.GetComponent<NavMeshAgent>();
            context.sphereCollider = gameObject.GetComponent<SphereCollider>();
            context.boxCollider = gameObject.GetComponent<BoxCollider>();
            context.capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            context.characterController = gameObject.GetComponent<CharacterController>();
            
            // Add whatever else you need here...

            return context;
        }
    }
}