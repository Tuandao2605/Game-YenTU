# YÊN TỬ: PHÁT MÔN CỔ KÍNH

_(Yen Tu: Chronicles of the Ancient Path)_

Dự án game **Third-Person Action-Adventure RPG / Historical Fantasy / Puzzle** lấy bối cảnh danh sơn Yên Tử đương đại giao thoa với thời kỳ Hồng Đức (Đại Việt). Hỗ trợ chơi đơn (Solo) hoặc phối hợp 1–4 người (Online Co-op).

---

## 1. Yêu cầu Môi trường & Kỹ thuật

- **Unity Version:** `Unity 6 (6000.4.6f1)` (hoặc bản Unity 6 LTS tương thích).
- **Render Pipeline:** Universal Render Pipeline (URP).
- **Input System:** New Input System (`com.unity.inputsystem`).
- **Version Control:** Git tích hợp **Git LFS** (Large File Storage).

---

## 2. Hướng dẫn Khởi tạo cho Thành viên (Setup Guide)

### Bước 1: Cài đặt Git & Git LFS

Trước khi clone repo, đảm bảo máy tính đã cài đặt **Git LFS**:

```bash
# Kiểm tra hoặc cài đặt Git LFS
git lfs install
```

### Bước 2: Clone Repository

```bash
git clone https://github.com/Tuandao2605/Game-YenTU.git
cd Game-YenTU
git lfs pull
```

### Bước 3: Chuyển sang nhánh phát triển (`dev`)

```bash
git checkout dev
```

### Bước 4: Mở dự án trong Unity Editor

- Khởi động **Unity Hub**.
- Chọn **Add project from disk** và trỏ tới thư mục `Game-YenTU`.
- Chọn đúng phiên bản **Unity 6 (6000.4.6f1)**.
- Đảm bảo khi mở không bị báo lỗi thiếu Packages (Unity Package Manager sẽ tự động restore các package từ `manifest.json`).

---

## 3. Chiến lược Phân nhánh Git (Branching Strategy)

Dự án áp dụng mô hình Git-Flow tinh gọn nhằm đảm bảo tính ổn định và tránh merge conflict:

```
main (Bản build ổn định / Demo / Release)
  ▲
  │ (Pull Request khi hoàn thành Milestone)
dev (Nhánh tích hợp chung - Default development branch)
  ▲
  │ (Pull Request & Code Review)
feature/<name>-<task-description> (Nhánh làm việc cá nhân)
```

### Quy tắc làm việc trên nhánh:

1. **`main`:**
   - Chỉ chứa các phiên bản đã kiểm thử ổn định, sẵn sàng build.
   - **Nghiêm cấm push trực tiếp** vào `main`.
2. **`dev`:**
   - Nhánh tích hợp làm việc chung của cả nhóm.
   - Mọi feature mới sau khi hoàn thành sẽ được tạo Pull Request (PR) để merge vào `dev`.
3. **`feature/<name>-<task-description>`:**
   - Nhánh cá nhân thực hiện từng task cụ thể.
   - Ví dụ:
     - `feature/A-player-combat`
     - `feature/B-boss-bach-ho`
     - `feature/C-puzzle-light-beam`

### Quy trình tạo và đẩy nhánh:

```bash
# 1. Cập nhật nhánh dev mới nhất
git checkout dev
git pull origin dev

# 2. Tạo nhánh feature mới
git checkout -b feature/A-player-controller

# 3. Làm việc, commit và push lên remote
git add .
git commit -m "feat(characters): implement basic movement and sprint"
git push -u origin feature/A-player-controller

# 4. Tạo Pull Request trên GitHub vào nhánh dev
```

---

## 4. Cấu trúc Thư mục Dự án (`Assets/_Project/`)

Tất cả tài nguyên do nhóm phát triển **bắt buộc** phải đặt trong thư mục `Assets/_Project/` để tách biệt với thư mục Plugins/Packages bên ngoài:

```
Assets/_Project/
├── Core/
│   ├── Scripts/               # GameManager, StateMachine, EventBus, Base Classes
│   └── Prefabs/               # Hệ thống quản lý cốt lõi
├── Characters/
│   ├── Scripts/               # PlayerController, Combat, Skills của Minh, Linh, Khang, An
│   ├── Prefabs/               # Prefabs nhân vật (PF_Player_Minh, ...)
│   └── Animations/            # Animator Controllers và Animation Clips
├── Enemies/
│   ├── Scripts/               # EnemyBase, Minion Logic
│   ├── Prefabs/               # PF_Enemy_UAnh, ...
│   └── AI/                    # Behavior Tree / State Machine quái thường
├── Bosses/
│   ├── Scripts/               # BossBase, Valheim-like scaling, Purification system
│   ├── Prefabs/               # Boss Prefabs
│   ├── BachHo/                # Script & Assets riêng của Bạch Ngạch Hổ Thần
│   └── LinhXa/                # Script & Assets riêng của Linh Mộc Mãng Xà
├── Networking/
│   ├── Scripts/               # NetworkManager Setup, NetworkVariables, RPCs
│   └── Prefabs/               # Network Prefabs (Lobby, Network Spawner)
├── Environment/
│   ├── Levels/                # Scene từng màn chơi (Suối Giải Oan, Hoa Yên, ...)
│   ├── LowPolyAssets/         # Mesh, 3D Models kiến trúc & thiên nhiên
│   └── Puzzles/               # Cơ quan giải đố (Gương đá, Trụ phong ấn)
└── UI/
    ├── HUD/                   # Máu, Thể lực, Linh Quang, Tà Ấn indicators
    ├── MainMenu/              # Giao diện menu chính
    └── Lobby/                 # Giao diện chọn nhân vật & phòng Co-op
```

---

## 5. Quy chuẩn Đặt tên (Naming Conventions)

### 5.1. Mã nguồn C#

- **File & Class Name:** Sử dụng `PascalCase`.
  - Ví dụ: `PlayerController.cs`, `BossBase.cs`, `PuzzleMirror.cs`.
- **Namespace:** Đặt theo cấu trúc module `YenTu.<ModuleName>`.
  - Ví dụ: `YenTu.Core`, `YenTu.Characters`, `YenTu.Bosses`, `YenTu.Networking`.
- **Variables:**
  - Private fields: `camelCase` (hoặc `_camelCase` nếu phân biệt).
  - Public properties: `PascalCase`.
  - SerializeField: `[SerializeField] private float moveSpeed;`.
- **Methods:** Sử dụng `PascalCase` (VD: `TakeDamage()`, `ApplyPlayerScaling()`).

### 5.2. Assets & Resources

Áp dụng tiền tố (prefix) theo chuẩn Unity chuẩn hóa:

| Loại Asset                            | Tiền tố (Prefix) | Ví dụ minh họa                                        |
| :------------------------------------ | :--------------- | :---------------------------------------------------- |
| **Prefab**                            | `PF_`            | `PF_Player_Minh`, `PF_Enemy_UAnh`, `PF_Altar_Incense` |
| **Scene**                             | `SC_`            | `SC_SuoiGiaiOan`, `SC_HoaYen_Hub`, `SC_ChuaDong`      |
| **Material**                          | `M_`             | `M_StylizedRock`, `M_Water_GiaiOan`, `M_Boss_BachHo`  |
| **Texture (Albedo)**                  | `T_..._D`        | `T_Wood_D`, `T_StoneWall_D`                           |
| **Texture (Normal)**                  | `T_..._N`        | `T_Wood_N`, `T_StoneWall_N`                           |
| **Texture (Mask/Metallic/Roughness)** | `T_..._MRA`      | `T_Armor_MRA`                                         |
| **Shader**                            | `SH_`            | `SH_StylizedFoliage`, `SH_CorruptedAura`              |
| **VFX / Particle**                    | `VFX_`           | `VFX_PurificationLight`, `VFX_TaAn_Burst`             |
| **Audio (Music)**                     | `BGM_`           | `BGM_SuoiGiaiOan`, `BGM_Boss_BachHo`                  |
| **Audio (SFX)**                       | `SFX_`           | `SFX_Sword_Slash_01`, `SFX_Parry_Success`             |
| **Animator Controller**               | `AC_`            | `AC_Player_Minh`, `AC_Boss_BachHo`                    |
| **Animation Clip**                    | `ANIM_`          | `ANIM_Minh_LightAttack_01`, `ANIM_Hổ_Roar`            |

---

## 6. Quy tắc Phối hợp & Tránh Xung đột (Conflict Prevention)

1. **Không chỉnh sửa chung một Scene:**
   - Tránh nhiều thành viên cùng sửa file `SampleScene.unity`.
   - Mỗi thành viên phụ trách một khu vực/tính năng hãy tạo Scene riêng để test hoặc lắp ghép thành Prefab trước khi đưa vào Scene chính.
2. **Ưu tiên làm việc trên Prefab Stage:**
   - Mọi thực thể (Player, Enemy, Mechanism) cần được đóng gói thành Prefab. Thay đổi logic nên diễn ra bên trong Prefab thay vì can thiệp trực tiếp vào scene hierarchy.
3. **Tuân thủ Git LFS:**
   - Các file binary lớn (`.fbx`, `.png`, `.wav`, `.prefab`, `.unity`) đã được cấu hình tự động qua file [`.gitattributes`](.gitattributes).
4. **Pull thường xuyên:**
   - Trước khi bắt đầu ca làm việc và trước khi mở PR, hãy luôn `git pull origin dev` để cập nhật các thay đổi mới nhất từ các thành viên khác.
