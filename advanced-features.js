/* ============================================
   ADVANCED SPECTRUM WEBSITE FEATURES
   Enhanced interactivity, animations, and utilities
   ============================================ */

/**
 * Counter Animation - Animates numbers on scroll
 */
class CounterAnimator {
  constructor() {
    this.counters = document.querySelectorAll('.counter-number');
    this.init();
  }

  init() {
    if (!this.counters.length) return;

    const observer = new IntersectionObserver((entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting && !entry.target.dataset.counted) {
          this.animateCounter(entry.target);
          entry.target.dataset.counted = 'true';
          observer.unobserve(entry.target);
        }
      });
    }, { threshold: 0.5 });

    this.counters.forEach((counter) => observer.observe(counter));
  }

  animateCounter(element) {
    const target = parseInt(element.textContent.replace(/\D/g, ''));
    const duration = 2000; // ms
    const steps = 60;
    const stepValue = target / steps;
    let current = 0;

    const increment = () => {
      current += stepValue;
      if (current < target) {
        element.textContent = Math.floor(current) + '+';
        requestAnimationFrame(increment);
      } else {
        element.textContent = target + '+';
      }
    };

    increment();
  }
}

/**
 * Scroll Animation Trigger - Shows elements on scroll
 */
class ScrollAnimationTrigger {
  constructor() {
    this.elements = document.querySelectorAll('.scroll-animate');
    this.init();
  }

  init() {
    if (!this.elements.length) return;

    const observer = new IntersectionObserver((entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('visible');
        }
      });
    }, { threshold: 0.2 });

    this.elements.forEach((el) => observer.observe(el));
  }
}

/**
 * Smooth Scroll Enhancement
 */
class SmoothScroll {
  constructor() {
    this.init();
  }

  init() {
    document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
      anchor.addEventListener('click', (e) => {
        const href = anchor.getAttribute('href');
        if (href === '#') return;

        e.preventDefault();
        const target = document.querySelector(href);
        if (target) {
          target.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
      });
    });
  }
}

/**
 * Tooltip Manager
 */
class TooltipManager {
  constructor() {
    this.init();
  }

  init() {
    document.querySelectorAll('[data-tooltip]').forEach((element) => {
      element.addEventListener('mouseenter', (e) => this.show(e.target));
      element.addEventListener('mouseleave', (e) => this.hide(e.target));
    });
  }

  show(element) {
    const tooltip = document.createElement('div');
    tooltip.className = 'tooltip';
    tooltip.textContent = element.dataset.tooltip;
    tooltip.style.cssText = `
      position: fixed;
      background: rgba(8, 37, 58, 0.95);
      color: #fff;
      padding: 0.6rem 0.9rem;
      border-radius: 8px;
      font-size: 0.85rem;
      z-index: 1000;
      pointer-events: none;
      animation: slideInUp 0.2s ease-out;
      backdrop-filter: blur(10px);
      border: 1px solid rgba(255, 255, 255, 0.1);
      max-width: 200px;
      word-wrap: break-word;
    `;

    document.body.appendChild(tooltip);
    this.positionTooltip(element, tooltip);
    element.tooltip = tooltip;
  }

  positionTooltip(element, tooltip) {
    const rect = element.getBoundingClientRect();
    const tooltipRect = tooltip.getBoundingClientRect();

    let top = rect.top - tooltipRect.height - 10;
    let left = rect.left + (rect.width - tooltipRect.width) / 2;

    if (top < 10) {
      top = rect.bottom + 10;
    }

    if (left < 10) {
      left = 10;
    } else if (left + tooltipRect.width > window.innerWidth) {
      left = window.innerWidth - tooltipRect.width - 10;
    }

    tooltip.style.top = top + 'px';
    tooltip.style.left = left + 'px';
  }

  hide(element) {
    if (element.tooltip) {
      element.tooltip.remove();
      element.tooltip = null;
    }
  }
}

/**
 * Lazy Loading Images
 */
class LazyLoadImages {
  constructor() {
    this.init();
  }

  init() {
    if (!('IntersectionObserver' in window)) {
      // Fallback for older browsers
      document.querySelectorAll('img[data-src]').forEach((img) => {
        img.src = img.dataset.src;
      });
      return;
    }

    const observer = new IntersectionObserver((entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          const img = entry.target;
          img.src = img.dataset.src;
          img.classList.add('loaded');
          observer.unobserve(img);
        }
      });
    });

    document.querySelectorAll('img[data-src]').forEach((img) => {
      observer.observe(img);
    });
  }
}

/**
 * Theme Toggle - Dark/Light Mode
 */
class ThemeToggle {
  constructor() {
    this.key = 'spectrum-theme';
    this.init();
  }

  init() {
    const toggle = document.querySelector('[data-theme-toggle]');
    if (!toggle) return;

    const currentTheme = this.getTheme();
    toggle.addEventListener('click', () => this.toggleTheme());
    this.setTheme(currentTheme);
  }

  getTheme() {
    const saved = localStorage.getItem(this.key);
    if (saved) return saved;

    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  }

  toggleTheme() {
    const current = this.getTheme();
    const next = current === 'dark' ? 'light' : 'dark';
    this.setTheme(next);
    localStorage.setItem(this.key, next);
  }

  setTheme(theme) {
    document.documentElement.setAttribute('data-theme', theme);
    const toggle = document.querySelector('[data-theme-toggle]');
    if (toggle) {
      toggle.setAttribute('aria-pressed', theme === 'dark');
    }
  }
}

/**
 * Form Validation
 */
class FormValidator {
  constructor(formSelector = 'form') {
    this.forms = document.querySelectorAll(formSelector);
    this.init();
  }

  init() {
    this.forms.forEach((form) => {
      form.addEventListener('submit', (e) => this.handleSubmit(e));

      form.querySelectorAll('input, textarea, select').forEach((field) => {
        field.addEventListener('blur', () => this.validateField(field));
        field.addEventListener('input', () => {
          if (field.dataset.error) {
            this.validateField(field);
          }
        });
      });
    });
  }

  validateField(field) {
    const wrapper = field.closest('.modern-input') || field.parentElement;
    const message = wrapper.querySelector('.input-message');

    if (field.required && !field.value.trim()) {
      this.setError(field, 'This field is required', wrapper, message);
      return false;
    }

    if (field.type === 'email' && field.value && !this.isValidEmail(field.value)) {
      this.setError(field, 'Please enter a valid email', wrapper, message);
      return false;
    }

    if (field.minLength && field.value.length < field.minLength) {
      this.setError(field, `Minimum ${field.minLength} characters required`, wrapper, message);
      return false;
    }

    this.clearError(field, wrapper, message);
    return true;
  }

  setError(field, message, wrapper, messageEl) {
    field.classList.add('input-error');
    field.classList.remove('input-success');
    field.dataset.error = 'true';

    if (messageEl) {
      messageEl.textContent = message;
      messageEl.className = 'input-message error';
    }
  }

  clearError(field, wrapper, messageEl) {
    field.classList.remove('input-error');
    field.classList.add('input-success');
    delete field.dataset.error;

    if (messageEl) {
      messageEl.textContent = '✓ Valid';
      messageEl.className = 'input-message success';
    }
  }

  isValidEmail(email) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }

  handleSubmit(e) {
    const form = e.target;
    let isValid = true;

    form.querySelectorAll('input, textarea, select').forEach((field) => {
      if (!this.validateField(field)) {
        isValid = false;
      }
    });

    if (!isValid) {
      e.preventDefault();
    }
  }
}

/**
 * Notification System
 */
class NotificationSystem {
  static show(message, type = 'info', duration = 4000) {
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.style.cssText = `
      position: fixed;
      bottom: 20px;
      right: 20px;
      padding: 1rem 1.5rem;
      background: ${this.getBackgroundColor(type)};
      color: white;
      border-radius: 12px;
      box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
      animation: slideInUp 0.3s ease-out;
      z-index: 9999;
      max-width: 400px;
      border-left: 4px solid ${this.getAccentColor(type)};
      font-weight: 600;
      backdrop-filter: blur(10px);
    `;

    notification.textContent = message;
    document.body.appendChild(notification);

    if (duration) {
      setTimeout(() => {
        notification.style.animation = 'slideInUp 0.3s ease-out reverse';
        setTimeout(() => notification.remove(), 300);
      }, duration);
    }
  }

  static getBackgroundColor(type) {
    const colors = {
      success: 'rgba(29, 184, 84, 0.9)',
      error: 'rgba(220, 53, 69, 0.9)',
      warning: 'rgba(255, 193, 7, 0.9)',
      info: 'rgba(84, 168, 219, 0.9)',
    };
    return colors[type] || colors.info;
  }

  static getAccentColor(type) {
    const colors = {
      success: '#1db854',
      error: '#dc3545',
      warning: '#ffc107',
      info: '#54a8db',
    };
    return colors[type] || colors.info;
  }
}

/**
 * Parallax Effect
 */
class ParallaxEffect {
  constructor() {
    this.elements = document.querySelectorAll('[data-parallax]');
    this.init();
  }

  init() {
    if (!this.elements.length) return;

    window.addEventListener('scroll', () => this.update(), { passive: true });
  }

  update() {
    const scrollY = window.scrollY;

    this.elements.forEach((element) => {
      const speed = element.dataset.parallax || 0.5;
      const offset = scrollY * speed;
      element.style.transform = `translateY(${offset}px)`;
    });
  }
}

/**
 * Performance Monitoring
 */
class PerformanceMonitor {
  static init() {
    if (!window.PerformanceObserver) return;

    // Largest Contentful Paint
    const lcpObserver = new PerformanceObserver((entryList) => {
      const entries = entryList.getEntries();
      const lastEntry = entries[entries.length - 1];
      console.log('LCP:', lastEntry.renderTime || lastEntry.loadTime);
    });
    lcpObserver.observe({ entryTypes: ['largest-contentful-paint'] });

    // First Input Delay
    const fidObserver = new PerformanceObserver((entryList) => {
      const entries = entryList.getEntries();
      entries.forEach((entry) => {
        console.log('FID:', entry.processingDuration);
      });
    });
    fidObserver.observe({ entryTypes: ['first-input'] });
  }
}

/**
 * Initialization - Run all features
 */
document.addEventListener('DOMContentLoaded', () => {
  new CounterAnimator();
  new ScrollAnimationTrigger();
  new SmoothScroll();
  new TooltipManager();
  new LazyLoadImages();
  new ThemeToggle();
  new FormValidator();
  new ParallaxEffect();
  PerformanceMonitor.init();

  console.log('✨ Spectrum Advanced Features Loaded Successfully');
});

/**
 * Utility Functions
 */
const Spectrum = {
  /**
   * Show notification
   */
  notify(message, type = 'info', duration = 4000) {
    NotificationSystem.show(message, type, duration);
  },

  /**
   * Debounce function
   */
  debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
      const later = () => {
        clearTimeout(timeout);
        func(...args);
      };
      clearTimeout(timeout);
      timeout = setTimeout(later, wait);
    };
  },

  /**
   * Throttle function
   */
  throttle(func, limit) {
    let inThrottle;
    return function (...args) {
      if (!inThrottle) {
        func.apply(this, args);
        inThrottle = true;
        setTimeout(() => {
          inThrottle = false;
        }, limit);
      }
    };
  },

  /**
   * Get element position
   */
  getOffset(element) {
    const rect = element.getBoundingClientRect();
    return {
      top: rect.top + window.scrollY,
      left: rect.left + window.scrollX,
      width: rect.width,
      height: rect.height,
    };
  },

  /**
   * Check if element is in viewport
   */
  isInViewport(element) {
    const rect = element.getBoundingClientRect();
    return (
      rect.top >= 0 &&
      rect.left >= 0 &&
      rect.bottom <= window.innerHeight &&
      rect.right <= window.innerWidth
    );
  },

  /**
   * Add class on scroll
   */
  addClassOnScroll(selector, className) {
    const observer = new IntersectionObserver((entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add(className);
        } else {
          entry.target.classList.remove(className);
        }
      });
    });

    document.querySelectorAll(selector).forEach((el) => observer.observe(el));
  },

  /**
   * Animation frame loop
   */
  onAnimationFrame(callback) {
    let id;
    const loop = () => {
      callback();
      id = requestAnimationFrame(loop);
    };
    id = requestAnimationFrame(loop);
    return () => cancelAnimationFrame(id);
  },
};

// Export for external use
if (typeof window !== 'undefined') {
  window.Spectrum = Spectrum;
}
