# 🎯 Card Reordering Guide - SequestBio UI

## Overview
The SequestBio UI uses a tier-based layout system that makes it extremely easy to reorganize cards while maintaining professional visual hierarchy.

## 🏗️ Tier Structure

### **Tier 1: Critical Decision Points**
- **Purpose**: Primary risk assessment and treatment direction
- **Layout**: 2 columns (col-lg-6)
- **Visual**: Largest emphasis, blue gradient headers, scaled cards
- **Cards**: Proprietary Risk Score, Tumor Immune Status

### **Tier 2: Supporting Information**
- **Purpose**: Clinical context and outcome predictions
- **Layout**: 3 columns (col-lg-4)
- **Visual**: Medium emphasis, standard headers
- **Cards**: Patient Summary, Tumor Microenvironment, Predicted Outcomes

### **Tier 3: Analytical Insights**
- **Purpose**: Risk factors and performance benchmarks
- **Layout**: 2 columns (col-lg-6)
- **Visual**: Standard emphasis, gray headers
- **Cards**: Key Risk Contributors, Benchmark Comparison

### **Tier 4: Advanced Analysis**
- **Purpose**: Feature contribution analysis
- **Layout**: Single column (col-12)
- **Visual**: Wide format, purple gradient header
- **Cards**: SHAP Waterfall Analysis

### **Tier 5: Detailed Insights**
- **Purpose**: Comprehensive pathway and treatment analysis
- **Layout**: Single column (col-12)
- **Visual**: Comprehensive format, green gradient header
- **Cards**: Tabbed Insights

## 🔄 Easy Reordering Methods

### Method 1: Move Entire Tier Sections
Simply cut and paste entire `<div class="results-tier">` sections:

```html
<!-- Move this entire block to reorder tiers -->
<div class="results-tier tier-3">
    <div class="tier-header">
        <h3 class="tier-title">Analytical Insights</h3>
        <p class="tier-subtitle">Risk factors and performance benchmarks</p>
    </div>
    <div class="row g-4">
        <!-- Cards here -->
    </div>
</div>
```

### Method 2: Move Individual Cards Between Tiers
Move individual card wrappers between tier sections:

```html
<!-- Move this card wrapper to different tier -->
<div class="col-lg-6">
    <div class="card-wrapper analytical-card">
        <KeyRiskContributorsCard IsLoading="@isLoading" Model="@keyRiskContributorsModel" />
    </div>
</div>
```

### Method 3: Change Card Emphasis (Visual Hierarchy)
Change the wrapper class to instantly change visual emphasis:

```html
<!-- Change 'supporting-card' to 'critical-card' for highest emphasis -->
<div class="card-wrapper critical-card">
    <PatientSummaryCard IsLoading="@isLoading" Model="@patientSummaryModel" />
</div>
```

## 🎨 Visual Hierarchy Classes

### **critical-card**
- Blue gradient header
- Thick blue border
- Scaled appearance (1.02x)
- Strongest shadow
- **Use for**: Most important decision-making cards

### **supporting-card**
- Standard gray header
- Medium shadow
- Standard size
- **Use for**: Important context cards

### **analytical-card**
- Standard gray header
- Light shadow
- Standard size
- **Use for**: Analysis and comparison cards

### **advanced-card**
- Purple gradient header
- Standard shadow
- **Use for**: Complex analysis cards

### **detailed-card**
- Green gradient header
- Large minimum height
- **Use for**: Comprehensive information cards

## 📐 Column Layout Options

### 2-Column Layout
```html
<div class="col-lg-6">
    <div class="card-wrapper [card-type]">
        <!-- Card Component -->
    </div>
</div>
```

### 3-Column Layout
```html
<div class="col-lg-4">
    <div class="card-wrapper [card-type]">
        <!-- Card Component -->
    </div>
</div>
```

### Single Column Layout
```html
<div class="col-12">
    <div class="card-wrapper [card-type]">
        <!-- Card Component -->
    </div>
</div>
```

## 🔧 Quick Reordering Examples

### Example 1: Make Patient Summary Critical
```html
<!-- FROM: Supporting tier -->
<div class="col-lg-4">
    <div class="card-wrapper supporting-card">
        <PatientSummaryCard IsLoading="@isLoading" Model="@patientSummaryModel" />
    </div>
</div>

<!-- TO: Critical tier -->
<div class="col-lg-6">
    <div class="card-wrapper critical-card">
        <PatientSummaryCard IsLoading="@isLoading" Model="@patientSummaryModel" />
    </div>
</div>
```

### Example 2: Swap Card Positions
```html
<!-- Simply swap the card components within their wrappers -->
<div class="col-lg-6">
    <div class="card-wrapper critical-card">
        <TumorImmuneStatusCard IsLoading="@isLoading" Model="@tumorImmuneStatusModel" />
    </div>
</div>
<div class="col-lg-6">
    <div class="card-wrapper critical-card">
        <ProprietaryRiskScoreCard IsLoading="@isLoading" Model="@proprietaryModel" />
    </div>
</div>
```

### Example 3: Create New Tier
```html
<!-- Add new tier between existing ones -->
<div class="results-tier tier-2-5">
    <div class="tier-header">
        <h3 class="tier-title">Custom Analysis</h3>
        <p class="tier-subtitle">Specialized insights</p>
    </div>
    <div class="row g-4">
        <div class="col-lg-6">
            <div class="card-wrapper analytical-card">
                <!-- Your card here -->
            </div>
        </div>
    </div>
</div>
```

## 📱 Responsive Behavior

- **Desktop (1200px+)**: All layouts as designed
- **Tablet (768px-1199px)**: 3-column becomes 2+1, scaling reduced
- **Mobile (<768px)**: All cards single column, maintains tier order

## 🎯 Best Practices

1. **Keep related cards together** in the same tier
2. **Use critical-card sparingly** - only for primary decision points
3. **Maintain logical flow** - most important information first
4. **Test on mobile** after reordering
5. **Update tier titles** if you change tier content significantly

## 🚀 Quick Start Checklist

- [ ] Identify which cards to move
- [ ] Choose target tier based on importance
- [ ] Select appropriate column layout
- [ ] Pick visual hierarchy class
- [ ] Cut and paste card wrapper
- [ ] Test responsive behavior
- [ ] Update tier headers if needed

This system makes reorganization as simple as moving HTML blocks while automatically maintaining professional styling and responsive behavior! 