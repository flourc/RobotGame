using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUIClickFix : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas shopCanvas;
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    
    [Header("Debug")]
    [SerializeField] private bool logRaycastResults = false;
    
    private void Awake()
    {
        // Ensure we have an EventSystem in the scene
        CheckForEventSystem();
        
        // Initialize references if not set
        if (shopCanvas == null)
        {
            shopCanvas = GetComponentInParent<Canvas>();
        }
        
        if (graphicRaycaster == null && shopCanvas != null)
        {
            graphicRaycaster = shopCanvas.GetComponent<GraphicRaycaster>();
        }
        
        // Check canvas settings
        CheckCanvasSettings();
    }
    
    private void Start()
    {
        // Make sure all buttons are properly initialized and interactable
        InitializeButtons();
    }
    
    private void Update()
    {
        if (logRaycastResults && Input.GetMouseButtonDown(0))
        {
            DebugPointerRaycast();
        }
    }
    
    private void CheckForEventSystem()
    {
        // Check if EventSystem exists
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.LogError("No EventSystem found in the scene! Creating one now.");
            
            // Create EventSystem if missing
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }
    
    private void CheckCanvasSettings()
    {
        if (shopCanvas != null)
        {
            // For 3D scenes, check if Canvas is setup correctly
            if (shopCanvas.renderMode == RenderMode.ScreenSpaceCamera || 
                shopCanvas.renderMode == RenderMode.WorldSpace)
            {
                // If using camera space, make sure we have a camera reference
                if (shopCanvas.worldCamera == null)
                {
                    Debug.LogWarning("Canvas is in " + shopCanvas.renderMode + " mode but has no camera reference. Setting to main camera.");
                    shopCanvas.worldCamera = Camera.main;
                }
            }
            
            // Ensure canvas has a GraphicRaycaster
            if (graphicRaycaster == null)
            {
                Debug.LogWarning("Canvas has no GraphicRaycaster. Adding one now.");
                graphicRaycaster = shopCanvas.gameObject.AddComponent<GraphicRaycaster>();
            }
            
            // For better UI visibility in 3D games
            if (shopCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                Debug.Log("Consider setting Canvas to ScreenSpaceOverlay for UI that should appear on top of everything.");
            }
        }
        else
        {
            Debug.LogError("No Canvas found! Shop UI needs to be inside a Canvas.");
        }
    }
    
    private void InitializeButtons()
    {
        // Find all buttons in the shop
        Button[] buttons = GetComponentsInChildren<Button>(true);
        
        foreach (Button button in buttons)
        {
            // Ensure button is interactable
            if (!button.interactable)
            {
                Debug.LogWarning("Button '" + button.name + "' is not interactable. Enabling it.");
                button.interactable = true;
            }
            
            // Check if button has a valid target graphic
            if (button.targetGraphic == null)
            {
                Debug.LogWarning("Button '" + button.name + "' has no target graphic. Adding Image component.");
                Image image = button.GetComponent<Image>();
                if (image == null)
                {
                    image = button.gameObject.AddComponent<Image>();
                    // Make it semi-transparent white
                    image.color = new Color(1, 1, 1, 0.5f);
                }
                button.targetGraphic = image;
            }
            
            // Verify button has click handler
            if (!button.onClick.GetPersistentEventCount().Equals(0))
            {
                Debug.Log("Button '" + button.name + "' has " + button.onClick.GetPersistentEventCount() + " click handlers.");
            }
            else
            {
                Debug.LogWarning("Button '" + button.name + "' has no click handlers!");
            }
        }
    }
    
    private void DebugPointerRaycast()
    {
        // Create a pointer event for testing raycast
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        
        // Raycast using the Graphics Raycaster
        System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
        
        if (graphicRaycaster != null)
        {
            graphicRaycaster.Raycast(pointerData, results);
            
            // Log results
            Debug.Log("Raycast results count: " + results.Count);
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit: " + result.gameObject.name);
            }
        }
        else
        {
            Debug.LogError("No GraphicRaycaster available for debug.");
        }
    }
}