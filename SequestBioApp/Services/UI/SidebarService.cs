// Services/UI/SidebarService.cs
using Microsoft.JSInterop;

namespace SequestBioApp.Services.UI;

/// <summary>
/// Service for managing sidebar state and responsive behavior
/// </summary>
public class SidebarService
{
    private readonly IJSRuntime _jsRuntime;
    private bool _isOpen = true;
    private bool _isMobile = false;

    public event Action<bool>? SidebarStateChanged;
    public event Action<bool>? MobileStateChanged;

    public SidebarService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <summary>
    /// Current sidebar open/closed state
    /// </summary>
    public bool IsOpen => _isOpen;

    /// <summary>
    /// Whether the current viewport is mobile-sized
    /// </summary>
    public bool IsMobile => _isMobile;

    /// <summary>
    /// Toggle sidebar open/closed
    /// </summary>
    public void ToggleSidebar()
    {
        _isOpen = !_isOpen;
        SidebarStateChanged?.Invoke(_isOpen);
    }

    /// <summary>
    /// Explicitly set sidebar state
    /// </summary>
    public void SetSidebarState(bool isOpen)
    {
        if (_isOpen != isOpen)
        {
            _isOpen = isOpen;
            SidebarStateChanged?.Invoke(_isOpen);
        }
    }

    /// <summary>
    /// Initialize mobile detection and resize listener
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            // Check initial window size
            _isMobile = await _jsRuntime.InvokeAsync<bool>("window.matchMedia", "(max-width: 768px)").AsTask();
            
            // Set initial sidebar state based on screen size
            if (_isMobile)
            {
                _isOpen = false;
            }

            // Create resize listener
            await _jsRuntime.InvokeVoidAsync("window.addEventListener", "resize", 
                DotNetObjectReference.Create(this));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing SidebarService: {ex.Message}");
        }
    }

    /// <summary>
    /// Handle window resize events from JavaScript
    /// </summary>
    [JSInvokable]
    public async Task OnWindowResize()
    {
        try
        {
            var wasMobile = _isMobile;
            _isMobile = await _jsRuntime.InvokeAsync<bool>("window.matchMedia", "(max-width: 768px)").AsTask();

            if (wasMobile != _isMobile)
            {
                MobileStateChanged?.Invoke(_isMobile);
                
                // Auto-adjust sidebar state on mobile/desktop transition
                if (_isMobile && _isOpen)
                {
                    SetSidebarState(false);
                }
                else if (!_isMobile && !_isOpen)
                {
                    SetSidebarState(true);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling window resize: {ex.Message}");
        }
    }

    /// <summary>
    /// Close sidebar (typically used for mobile overlay clicks)
    /// </summary>
    public void CloseSidebar()
    {
        SetSidebarState(false);
    }

    /// <summary>
    /// Open sidebar
    /// </summary>
    public void OpenSidebar()
    {
        SetSidebarState(true);
    }
}